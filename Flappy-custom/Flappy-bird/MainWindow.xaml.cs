using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Flappy_bird
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string musicurl;
		// create a new instance of the timer class called game timer
		DispatcherTimer gameTimer = new DispatcherTimer();
		// new gravity integer hold the vlaue 5
		int gravity = 5;
		// score keeper
		double score;
		// new rect class to help us detect collisions
		Rect FlappyRect;
		// new boolean for checking if the game is over or not
		bool gameOver = false;
		public MainWindow()
		{
			InitializeComponent();

			// set the default settings for the timer
			gameTimer.Tick += gameEngine; // link the timer tick to the game engine event
			gameTimer.Interval = TimeSpan.FromMilliseconds(30); // set the interval to 20 miliseconds
																// run the start game function
			startGame();

		}

		private void MainEventTimer(object sender, EventArgs e)
		{
		}
		private void PlaySound(string musicurl)
		{
			SoundPlayer player = new SoundPlayer(musicurl);
			player.Load();
			player.Play();
		}
		private void KeyIsDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Space)
			{
				// rotate the bird image to -20 degrees from the center position
				flappyBird.Source = new BitmapImage(new Uri(@"images/flyingDun.png", UriKind.RelativeOrAbsolute));
				flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
				PlaySound(@"sounds/fart.wav");
				Image newfart = new Image();
				newfart.Source = new BitmapImage(new Uri(@"images/fart.png", UriKind.RelativeOrAbsolute));
				newfart.Tag = "newFart";
				MyCanvas.Children.Add(newfart);
				Canvas.SetTop(newfart, Canvas.GetTop(flappyBird) + 37);
				Canvas.SetLeft(newfart, Canvas.GetLeft(flappyBird) + 5);

				//musicurl = @"sounds/fart.wav";
				//PlaySound(musicurl);
				// change gravity so it will move upwards
				gravity = -5;
			}
			if (e.Key == Key.R && gameOver==true)
			{
				startGame();
			}
			if (e.Key == Key.Escape)
			{
				this.Close();
				MessageBox.Show("Lew lew Dũn gà không phá đảo được game.");
			}
		}

		private void KeyisUp(object sender, KeyEventArgs e)
		{
			flappyBird.Source = new BitmapImage(new Uri(@"images/flappyDun.png", UriKind.RelativeOrAbsolute));
			// if the keys are released then we will change the rotation of the flappy bird to 5 degrees from the center
			flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
			// change the gravity to 8 so the bird will go downwards and not up
			gravity = 5;
		}

		private void startGame()
		{
			MyCanvas.Focus();
			int temp = 300;
			score = 0;
			gameOver = false;

			Canvas.SetTop(flappyBird, 200);
			// the loop below will simply check for each image in this game and set them to their default positions
			foreach (var x in MyCanvas.Children.OfType<Image>())
			{
				// set obs1 pipes to its default position
				if (x is Image && (string)x.Tag == "obs1")
				{
					Canvas.SetLeft(x, 500);
				}
				// set obs2 pipes to its default position
				if (x is Image && (string)x.Tag == "obs2")
				{
					Canvas.SetLeft(x, 800);
				} 
				// set obs3 pipes to its default position
				if (x is Image && (string)x.Tag == "obs3")
				{
					Canvas.SetLeft(x, 1000);
				}
				// set the clouds to its default position
				if (x is Image && (string)x.Tag == "clouds")
				{
					Canvas.SetLeft(x, (300 + temp));
					temp = 800;
				}
			}
			gameTimer.Start();
		}
		private void gameEngine(object sender, EventArgs e)
		{
			// this is the game engine event linked to the timer
			// first update the score text label with the score integer
			txtScore.Content = "Score: " + score;
			// link the flappy bird image to the flappy rect class
			FlappyRect = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 15, flappyBird.Height - 10);
			// set the gravity to the flappy bird image in the canvas
			Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

			// check if the bird has either gone off the screen from top or bottom
			if (Canvas.GetTop(flappyBird) + flappyBird.Height > 490 || Canvas.GetTop(flappyBird) < -30)
			{
				musicurl = @"sounds/dead.wav";
				PlaySound(musicurl);
				// if it has then we end the game and show the reset game text
				gameTimer.Stop();
				gameOver = true;
				txtScore.Content += "   Press R to Try Again";
				MessageBox.Show($"Your Score: {score}  , Dũn noan!");
			}
			// below is the main loop, this loop will go through each image in the canvas
			// if it finds any image with the tags and follow the instructions with them

			foreach (var x in MyCanvas.Children.OfType<Image>())
			{
				if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")
				{
					// if we found an image with the tag obs1,2 or 3 then we will move it towards left of the screen
					Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);
					// create a new rect called pillars and link the rectangles to it
					Rect pillars = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
					// if the flappy rect and the pillars rect collide
					if (FlappyRect.IntersectsWith(pillars))
					{
						PlaySound(@"sounds/dead.wav");
						flappyBird.Source = new BitmapImage(new Uri(@"images/deadDun.png", UriKind.RelativeOrAbsolute));
						// stop the timer, set the game over to true and show the reset text
						gameTimer.Stop();
						gameOver = true;
						txtScore.Content += "   Press R to Try Again";
						MessageBox.Show($"Your Score: {score}  , Dũng noan.");
					}
				}
				// if the first layer of pipes leave the scene and go to -50 pixels from the left
				// we need to reset the pipe to come back again
				//add random px
				Random randompx = new Random();
				if ((string)x.Tag == "obs1" && Canvas.GetLeft(x) < -50)
				{
					// reset the pipe to 700 pixels
					Canvas.SetLeft(x, 750);
					int pxValue = randompx.Next(-80, 80);
					Canvas.SetTop(x, Canvas.GetTop(x) + pxValue);
					// add 1 to the score
					score = score + .5;
				}
				// if the second layer of pipes leave the scene and go to -200 pixels from the left
				if ((string)x.Tag == "obs2" && Canvas.GetLeft(x) < -50)
				{
					// we set that pipe to 700 pixels
					Canvas.SetLeft(x, 750);
					int pxValue = randompx.Next(-50, 50);
					Canvas.SetTop(x, Canvas.GetTop(x) + pxValue);
					// add 1 to the score
					score = score + .5;
				}
				// if the third layer of pipes leave the scene and go to -250 pixels from the left
				if ((string)x.Tag == "obs3" && Canvas.GetLeft(x) < -50)
				{
					// we set the pipe to 700 pixels
					Canvas.SetLeft(x, 750);
					int pxValue = randompx.Next(-50, 50);
					Canvas.SetTop(x, Canvas.GetTop(x) + pxValue);
					// add 1 to the score
					score = score + .5;
				}
				// if find any of the images with the clouds tag on it
				if ((string)x.Tag == "clouds")
				{
					// then we will slowly move the cloud towards left of the screen
					Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);
					// if the clouds have reached -220 pixels then we will reset it
					if (Canvas.GetLeft(x) < -50)
					{
						int pxValue = randompx.Next(-50, 50);
						Canvas.SetTop(x, Canvas.GetTop(x) + pxValue);
						// reset the cloud images to 550 pixels
						Canvas.SetLeft(x, 550);
					}
				}
				if ((string)x.Tag == "newFart")
				{
					Canvas.SetTop(x, Canvas.GetTop(x) + 10);
				}

			}
			if (score >= 5)
			{
				gameTimer.Interval = TimeSpan.FromMilliseconds(25);
			}
			if (score >= 10)
			{
				gameTimer.Interval = TimeSpan.FromMilliseconds(20);
			}
			if (score >= 15)
			{
				gameTimer.Interval = TimeSpan.FromMilliseconds(15);
			}
			if (score >= 20)
			{
				gameTimer.Interval = TimeSpan.FromMilliseconds(10);
			}
			if (score >= 30)
			{
				gameTimer.Interval = TimeSpan.FromMilliseconds(10);
				MessageBox.Show("Ghê vậy sao, z là ghê rồi :)) ");
			}
		}

	}
}
