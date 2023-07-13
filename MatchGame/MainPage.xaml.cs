using Microsoft.Maui.Controls;

namespace MatchGame;
///<Summary>
///<Createddate>2023/07/11</Createddate>
///<company>SandBox</company>
///<lastmodificationdate>2023/07/12</lastmodificationdate>
///<lastmodificationdescription>
///Agregar la documentacion 
///</lastmodificationdescription>
///<lastmodifierautor>Lorena Vasquez</lastmodifierautor>
///</Summary>

public partial class MainPage : ContentPage
{
	/// <summary>
	/// Constructor de la clase.
	/// </summary>

	public MainPage()
	{
		InitializeComponent();
		SetUpGame();
	}
	/// <summary>
	/// Luego realizamos un listado de los emojis que se mostraran en el button, 
	/// que conforma el juego.
	/// </summary>
	public void SetUpGame()
	{
		List<string> animalEmoji = new List<string>()
		{
			"🐳","🐳",
			"🐹","🐹",
			"🐨","🐨",
			"🐱","🐱",
			"🐿","🐿",
			"🐣","🐣",
			"🐻","🐻",
			"🐰","🐰",

		};
		
		Random random = new Random();
		foreach (Button view in Grid1.Children)
		{
			int index = random.Next(animalEmoji.Count);

			string nextEmoji = animalEmoji[index];

			view.Text = nextEmoji;

			animalEmoji.RemoveAt(index);
		}
	}

	Button ultimoButtonClicked;

	bool encontrandoMatch = false;

	/// <summary>
	/// Este codigo lo que hara es que si los dos emojis son iguales (mejor dicho hacen match)
	/// ambos desapareceran, pero si no lo son, no desaparecera y se enviara un mensaje de error
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Button_Clicked(object sender, EventArgs e)
	{
		Button button = sender as Button;
		if (encontrandoMatch == false)
		{
			button.IsVisible = false;
			ultimoButtonClicked = button;
			encontrandoMatch = true;

		}
		else if (button.Text == ultimoButtonClicked.Text)
		{
			button.IsVisible = false;
			encontrandoMatch = false;

		}
		else
		{
			ultimoButtonClicked.IsVisible = true;
			encontrandoMatch = false;
			DisplayAlert("NO!!!", "te has equivocado, pero puedes volver a intentar", "Aceptar");
		}
	}


	private TimeOnly time = new();
	private bool isRunning;

	private void setTime()
	{
		Timer.Text = $"{time.Minute}:{time.Second:000}";
		
	}


	/// <summary>
	/// con este boton lo que se hara sera iniciar o detener el tiempo del temporizador.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private async void Inicio_Clicked(object sender, EventArgs e)
	{
		isRunning = !isRunning;
		Inicio.Text = isRunning ? "pause" : "play";

		while (isRunning)
		{
			time = time.Add(TimeSpan.FromSeconds(1));
			setTime();
			await Task.Delay(TimeSpan.FromSeconds(1));
		}
	
	}
	/// <summary>
	/// con esto lo que haremos es que se reinicie el juego y el temporizador
	/// luego se enviara un mensaje de que el juego se reinicio exitosamente.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void restart_Clicked(object sender, EventArgs e)
	{
		time = new TimeOnly();
		setTime();
		Navigation.PushModalAsync(new MainPage());
		DisplayAlert("Felicidades", "El juego se ha reiniciado exitosamente", "Aceptar");
	}
} 

