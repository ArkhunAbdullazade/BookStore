using BookStore.Models;
using BookStore.Repositories.Base;
using BookStore.Repositories;
using BookStore.Services.Base;
using BookStore.Services;
using BookStore.ViewModels.Base;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows;
using SimpleInjector;
using CarShop.Repositories;
using BookStore.Data;
using BookStore.Repositories.ForComments.Base;
using BookStore.Repositories.ForComments;
using System.ComponentModel.Design;
using System.Text.Json;
using System.Security.RightsManagement;
using System.IO;
using Microsoft.IdentityModel.Tokens;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container ServiceContainer { get; set; } = new Container();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            var context = ServiceContainer.GetInstance<BookStoreDBContext>();
            if (context.Books.IsNullOrEmpty())
            {
                ConfigureBooks(context);
            }
            StartWindow<LogInViewModel>();
        }

        private void ConfigureContainer()
        {
            ServiceContainer.RegisterSingleton<IMessenger, Messenger>();
            ServiceContainer.RegisterSingleton<IUsersRepository<User>, UserSqlRepository>();
            ServiceContainer.RegisterSingleton<IBooksRepository<Book>, BookSqlRepository>();
            ServiceContainer.RegisterSingleton<IUserBooksRepository<UserBook>, UserBookSqlRepository>();
            ServiceContainer.RegisterSingleton<ICommentsRepository<Comment>, CommentSqlRepository>();
            ServiceContainer.RegisterSingleton<BookStoreDBContext>();

            ServiceContainer.RegisterSingleton<MainViewModel>();
            ServiceContainer.RegisterSingleton<MenuViewModel>();
            ServiceContainer.RegisterSingleton<LogInViewModel>();
            ServiceContainer.RegisterSingleton<SignUpViewModel>();
            ServiceContainer.RegisterSingleton<HomeViewModel>();
            ServiceContainer.RegisterSingleton<ProfileViewModel>();
            ServiceContainer.RegisterSingleton<ShopViewModel>();
            ServiceContainer.RegisterSingleton<LibraryViewModel>();
            ServiceContainer.RegisterSingleton<AboutViewModel>();
            ServiceContainer.RegisterSingleton<BookViewModel>();

            ServiceContainer.Verify();
        }

        private void StartWindow<T>() where T : ViewModelBase
        {
            var startView = new MainWindow();

            var startViewModel = ServiceContainer.GetInstance<MainViewModel>();
            startViewModel.ActiveViewModel = ServiceContainer.GetInstance<T>();
            startView.DataContext = startViewModel;

            startView.ShowDialog();
        }

        private void ConfigureBooks(BookStoreDBContext context)
        {
            var json = File.ReadAllText("..\\..\\..\\Json Mock Data\\Books.json");
            Book[] Books = JsonSerializer.Deserialize<Book[]>(json);
            foreach (var book in Books)
            {
                context.Books.Add(book);
            }
            context.SaveChanges();
        }
    }
}
