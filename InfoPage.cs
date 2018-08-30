using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CardLayout
{
    public class InfoPage : ContentPage
    {
    // MARK: - Variable definition
    // The strings are defined as empty because they will be constructed depending on the device
        int index = 0;
        private readonly Image image = new Image();
        private readonly Label title = new Label();
        private readonly Label text = new Label();
        private readonly Image background = new Image()
        {
            Source = ImageSource.FromFile("background.png"),
            Aspect = Aspect.Fill
        };
        private readonly string fontLobster = "";
        private readonly string fontFira = "";
        private readonly string fontProxima = "";
        private readonly double top;
        private readonly string imageFileBack = "";
        private readonly string imageFileNext = "";
        
        // Infos from the Info Class created to display this pattern of information
        List<Info> infos = new List<Info>()
        {
            new Info () {
                Title = "Como começar",
                Text = "Use nosso app sem precisar de cadastros ou informações",
                FileName = "rocket.png",
            },
            new Info () {
                Title = "Funcionalidades",
                Text = "Faça notificações à ADAPI e ajude a combater doenças de importância sanitária, como a Febre Aftosa e Brucelose. " +
                "Amplo conteúdo atualizado sobre o órgão e as ações desenvolvidas.",
                FileName = "gear.png",
            },
            new Info () {
                Title = "Notificando",
                Text = "Utilize a plataforma para informar a ocorrência de doenças ou sintomas em animais de produção, como bovinos, caprinos, etc. " +
                "Insira os sinais observados, de forma coloquial ou técnica, caso seja um profissional da área.",
                FileName = "alert.png",
            },
            new Info () {
                Title = "Como notificar?",
                Text = "Anexe vídeos ou imagens diretamente do seu smartphone, ou apenas descreva o que observou. " +
                "Não quer ser identificado? Não tem problema! Utilize o recurso anônimo e não deixe de contribuir.",
                FileName = "speaker.png",
            },
            new Info () {
                Title = "Conteúdo exclusivo",
                Text = "Tenha acesso a vídeos e reportagens e esteja sempre informado sobre o cenário Agropecuário piauiense. " +
                "Personalize sua busca através de nossas tags e coleções",
                FileName = "newspaper.png",
            },
        };
        
        public InfoPage()
        {

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    top = 20;
                    imageFileBack = "Icons/chevron_back";
                    imageFileNext = "Icons/chevron_next";
                    fontLobster = "Lobster-Regular";
                    fontFira = "FiraSansCondensed-Light-webfont";
                    fontProxima = "Proxima-Nova";
					Icon = "Icons/ic_info_outline";
                    break;
                case Device.Android:
                    top = 0;
                    imageFileBack = "chevron_back.png";
                    imageFileNext = "chevron_next.png";
                    fontLobster = "LobsterRegular.ttf#LobsterRegular";
                    fontFira = "FiraSansCondensed-Light-webfont.ttf#FiraSansCondensed-Light-webfont";
					Icon = "ic_info_outline_white_24dp";
                    break;
                default:
                    top = 0;
                    break;
            }

            Padding = new Thickness(0, top, 0, 0);

            Title = "Info";

            // MARK: - This creates a navigation system using the back and next images as buttons
            
            Image imageButtonBack = new Image()
            {
                Source = ImageSource.FromFile(imageFileBack),
                BackgroundColor = Color.Transparent,
            };
            TapGestureRecognizer tapGestureRecognizerBack = new TapGestureRecognizer();
            tapGestureRecognizerBack.Tapped += (s, e) => {
                index = index > 0 ? index - 1 : infos.Count - 1;
                image.Source = infos[index].FileName;
                title.Text = infos[index].Title;
                text.Text = infos[index].Text;

                image.Opacity = 0;
                image.FadeTo(1, 4000);
            };
            imageButtonBack.GestureRecognizers.Add(tapGestureRecognizerBack);

            Image imageButtonNext = new Image()
            {
                Source = ImageSource.FromFile(imageFileNext),
                BackgroundColor = Color.Transparent,
            };
            TapGestureRecognizer tapGestureRecognizerNext = new TapGestureRecognizer();
            tapGestureRecognizerNext.Tapped += (s, e) => {
                index = index < infos.Count - 1 ? index + 1 : 0;
                image.Source = infos[index].FileName;
                image.Opacity = 0;
                image.FadeTo(1, 4000);
                title.Text = infos[index].Title;
                title.FontFamily = fontLobster;
                title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                text.Text = infos[index].Text;
                text.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            };
            imageButtonNext.GestureRecognizers.Add(tapGestureRecognizerNext);

            // MARK: - This creates the internal grid that unites the image displayed and the text
            // TODO: - Center the text
            
            Grid internalGrid = new Grid()
            {

                Padding = 0,
                RowSpacing = 0,
                ColumnSpacing = 0,

                RowDefinitions = {
                        new RowDefinition() { Height = new GridLength(0.30, GridUnitType.Star) },
                        new RowDefinition() { Height = new GridLength(0.60, GridUnitType.Star) },
                    },

                ColumnDefinitions = {
                        new ColumnDefinition() { Width = new GridLength(0.9, GridUnitType.Star) },
                    },

            };
            //left col, limit col, top row, limit row

            internalGrid.Children.Add(image, 0, 1, 0, 1);
            internalGrid.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { title, text }
            }, 0, 1, 1, 2);


            // MARK: - Exterior grid that joins the back and next button with the info
            
            Grid grid = new Grid()
            {
                Padding = 0,
                RowSpacing = 0,
                ColumnSpacing = 0,

                RowDefinitions = {
                    new RowDefinition() { Height = new GridLength(0.05, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(0.90, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(0.05, GridUnitType.Star) },
                },
                ColumnDefinitions = {
                    new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(0.70, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) }
                },
            };
            grid.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { imageButtonBack }
            }, 0, 1, 1, 2);
            grid.Children.Add(internalGrid, 1, 2, 1, 2);  // left col, limit col, top row, limit row
            grid.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { imageButtonNext }
            }, 2, 3, 1, 2);

            // MARK: - Wraps all the elements in a transparent card
            
            var frameCard = new Frame()
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.FromRgba(255, 255, 255, 0.8),
                BorderColor = Color.Transparent,
                HasShadow = true,
                CornerRadius = 20,

                Content = grid
            };
            
            // MARK: - Joins the background image and the card
            
            var layout = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutBounds(background, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(background, AbsoluteLayoutFlags.All);
            layout.Children.Add(background);

            AbsoluteLayout.SetLayoutBounds(frameCard, new Rectangle(0.5, 0.5, 0.9, 0.9));
            AbsoluteLayout.SetLayoutFlags(frameCard, AbsoluteLayoutFlags.All);
            layout.Children.Add(frameCard);
            Content = layout;
        }

        protected override void OnAppearing()
        {
            index = 0;
            image.Source = infos[index].FileName;
            title.Text = infos[index].Title;
            title.FontFamily = fontLobster;
            title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            text.FontFamily = fontProxima;
            text.Text = infos[index].Text;
        }
    }
}

