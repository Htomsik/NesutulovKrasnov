# __Практические работы по WPF 4 курс__

![Project Image](https://raw.githubusercontent.com/Htomsik/Htomsik/main/Assets/collage.png)


---

### Оглавление

- [Описание](#Описание)
- [Текущее состояние проекта](#Текущее-состояние-проекта)
- [Как использовать](#Как-использовать)
- [Ссылки на авторов](#Ссылки-на-авторов)

---

## __Описание__

Програма разрабатывается в рамках практических работ за первое полугодие 4-го курса специальности ПКС [Колледжа информатики и программирования Финансового университета при Правительстве Российской Федерации](http://www.fa.ru/org/spo/kip/Pages/Home.aspx)

>## __Приложение в данный момент находится в разработке__

### Встречающая пользователей страница регистрации

![Авторизация](https://raw.githubusercontent.com/Htomsik/Praktika4Kurs/master/ReadmyAssets/MainWindow.png)

### Страница регистрации

![Регистрация](https://raw.githubusercontent.com/Htomsik/Praktika4Kurs/master/ReadmyAssets/registrwindow.png)

### Главная страница приложения 
![Главное окно](https://raw.githubusercontent.com/Htomsik/Praktika4Kurs/master/ReadmyAssets/MainMenu.png)

#### Технологии

- .Net
- WPF

#### Паттерн

- mvvm

### nuget пакеты
- EntityFramework
- System.Windows.Interactivity.WPF
- FontAwesome5

---
## Текущее состояние проекта

- ### ПР1 "Подготовка базовой формы приложения":
   - [X] Создание основной формы с меню
   - [X] Создание формы авторизации
        - [X] Добавление асинхронного метода обращения к базе
        - [X] Добавление анимации загрузки во время ожидания ответа от сервера
        - [ ] Добавление passwordbox с возможностью биндинга 
        - [ ] Добавление правильной валидации данных
   - [X] Добавление словарей стиля
   - [X] Добавление переходов между окнами посредство relaycommand (страницами)
   - [X] Создание подключения к локальной бд (заменено на 
   Azure SQL)
   
- ### ПР2 "Реализация калькулятора на WPF":
    - [X] Создание формы калькулятора
    - [X] Создание базовой версии калькулятора
    - [X] Добавление функционала инженерного калькулятора
    - [ ] Перенос на MVVM 

- ### ПР3 "Реализация регистрации пользователей на WPF":
    - [X] Создание формы регистрации
    - [X] Модальное окно оповещающее об успешном создании
    - [X] Добавление асинхронного метода обращения к базе
    - [X] Добавление анимации загрузки во время ожидания ответа от сервера
    - [ ] Добавление passwordbox с возможностью биндинга
    - [ ] Добавление правильной валидации данных 
   
- ### Общее:
    - [X] Создание проекта на mvvm паттерне
    - [X] Добавление удаленной Azure SQL DB, вместо локальной
    - [X] Создание простой шины сообщения для переключения окон приложения

### Текущая диаграмма классов:
![classDiagram](https://raw.githubusercontent.com/Htomsik/Praktika4Kurs/master/ReadmyAssets/ClassDiagram.png)

> ##### P.s За верность соединений не ручаемся, опыт в построении диаграмм классов не особо большой
   

---

## __Как использовать__

- При входе в приложение открывается страница авторизации
    - Если отсутсвует аккаунт нужно зарегистрировать (ссылка на регистрацию находится на странице)
- После авторизации происходит вход в главное окно приложения где в боковом меню расположены ссылка на страницы с практическими работами

## __Установка__

[Ссылка на последнюю версию установщика](https://drive.google.com/drive/folders/1aGvLi4t4F5CvNTLdGAfF7jjk9sNt0ofy?usp=sharing) 

## __Ссылки на авторов__
[![Артём](https://img.shields.io/badge/-Артём-1C1C22?style=for-the-badge&logo=vk&logoColor=red)](https://vk.com/id506987182)
[![Костя](https://img.shields.io/badge/-Костя-1C1C22?style=for-the-badge&logo=vk&logoColor=blue)](https://vk.com/jessnjake)







