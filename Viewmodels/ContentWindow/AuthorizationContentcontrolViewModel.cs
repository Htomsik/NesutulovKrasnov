﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Praktika.Services;
using Praktika.Infrastructures.Commands;
using Praktika.Properties;


namespace Praktika.Viewmodels
{
    public class AuthorizationContentcontrolViewModel:BaseViewModel
    {

        public AuthorizationContentcontrolViewModel()
        {
            SendContentControlNumerCommand =
                new LambdaCommand(OnContentControlNumerExecuted, CanContentControlNumerExecute);

            OpenRegistrationCommand =
                new LambdaCommand(OnOpenRegistrationCommandExecuted, CanOpenRegistrationCommandExecute);

            JsInicializeCommand = new LambdaCommand(OnJsInicializeExecuted, CanJsInicializeExecute);

            Js = new JsonWorker();


        }

        private JsonWorker Js;

        #region Команды

        #region Инициализация текущего пользователя при авторизации


        public ICommand JsInicializeCommand { get; }

        private bool CanJsInicializeExecute(object p) => true;

        private async void OnJsInicializeExecuted(object p)
        {

            if (Js.GetSettings())
            {
                StartLoading();
                Js.GetUser();
                await Task.Run(() => Authorisation());
            }

        }

        #endregion

        #region Отправка номера страницы

        public ICommand SendContentControlNumerCommand { get; }

        private bool CanContentControlNumerExecute(object p) => CheckParametrs();

        private async void OnContentControlNumerExecuted(object p)
        {
            StartLoading();
            await Task.Run(() => Authorisation(p));
        }
        #endregion

        #region Переход на страницу регистрации

        public ICommand OpenRegistrationCommand { get; }

        private bool CanOpenRegistrationCommandExecute(object p) => true;

        private void OnOpenRegistrationCommandExecuted(object p)
        {
            MessageBus.Send(p);
        }
        #endregion

        #endregion

        #region Данные с формы

        private string _Login;
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }


        /// <summary>
        /// Видимость ошибки
        /// </summary>
        private Visibility _ErrorVisibility = Visibility.Hidden;
        public Visibility ErrorVisibility
        {
            get => _ErrorVisibility;
            set => Set(ref _ErrorVisibility, value);
        }

        /// <summary>
        /// Видимость грида с информацией
        /// </summary>
        private Visibility _MainGridVisibility = Visibility.Visible;
        public Visibility MainGridVisibility
        {
            get => _MainGridVisibility;
            set => Set(ref _MainGridVisibility, value);
        }

        /// <summary>
        /// Статус видимости загрузки
        /// </summary>
        private bool _LoadingStatus = false;
        public bool LoadingStatus
        {
            get => _LoadingStatus;
            set => Set(ref _LoadingStatus, value);
        }

        private bool _checkedstatus;
        /// <summary>
        /// Запоминает статус
        /// </summary>
        public bool checkedstatus
        {
            get => _checkedstatus;
            set => Set(ref _checkedstatus, value);
        }

        #endregion

        #region Методы

        #region Проверка заполненности параметров

        /// <summary>
        /// Проверка заполненности всех параметров
        /// </summary>
        /// <returns></returns>
        private bool CheckParametrs() => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);

        #endregion

        #region Авторизация с формы

        private async void Authorisation(object p)
        {
            //Если есть такой пользователь то открыть главную страницу
            if (DataWorker.Authorization(Login, Password))
            {
                await Task.Run(() => Js.SaveSettings(checkedstatus));
                Thread.Sleep(1000);
                MessageBus.Send(p);
            }
            else
            {
                //показ ошибки
                ErrorVisibility = Visibility.Visible;
            }
            EndLoading();
        }

        #endregion

        #region Авторизация с файла

        private void Authorisation()
        {
            //Если есть такой пользователь то открыть главную страницу
            if (DataWorker.Authorization())
            {
                Thread.Sleep(1000);
                MessageBus.Send(1);
            }
            else
            {
                //показ ошибки
                ErrorVisibility = Visibility.Visible;
            }
            EndLoading();
        }

        #endregion

        #region методы переключения видимости

        private void StartLoading()
        {
            
            MainGridVisibility = Visibility.Collapsed; //выключаю видимость грида с данными
            LoadingStatus = true; //включает анимацию
        }

        private void EndLoading()
        {
            Thread.Sleep(1000);
            MainGridVisibility = Visibility.Visible; //включает видимость грида с данными
            LoadingStatus = false; //выключает анимацию
        }

        #endregion

        #endregion

    }
}
