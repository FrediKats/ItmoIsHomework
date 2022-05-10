# Web-программирование (6 семестр, 18/19)

Ознакомиться с 1-3 работами можно по ссылке: https://inredikawb.github.io/itmo-labs-web/
Остальные требуют дополнительных усилий.

Лабораторная 4:
- установка https://ospanel.io/ (скорость обрезают, можете стучать в лс)
- для хостинга с подгрузкой используем команду `php artisan serve`

Лабораторная 5:
- установка https://nodejs.org/en/
- восстановление зависимостей `npm install`
- запуск клиента `npm run serve`
- запуск сервера (стоит сделать в отдельной консоли) `npm run server-nodemon`

## Лабораторная работа 1: Адаптивная верстка компоновки
### Задание
Нужно разработать адаптивную вёрстку компоновки (wireframe) web-приложения. В решении должно быть минимум 4 блока (например: профиль, общая информация, друзья, стена), которые определённым образом распределены по странице. Пользуясь медиа-запросом, нужно реализовать адаптивную верстку. При маленьких размерах экрана блоки растягиваются или центруются.

Полезные свойства CSS: display, position, width, height, float, background-color.

**Решение**: Адаптивность была реализована с помощью flex-box.


## Лабораторная работа 2: Одностраничный сайт
### Задание
Нужно сделать посадочную страницу для продукта или услуги с помощью CSS фреймворка (bootstrap или materialize). Страница должна состоять из следующих блоков: главная (желательно с call to action), краткое описание продукта/услуги, блок с тремя выделенными пунктами, подробное описание продукта/услуги, форма обратной связи. Блок с тремя выделенными пунктами должен быть реализован с помощью grid системы для разных размеров экрана. Форма обратной связи должна содержать элементы input и textarea. Страница должна быть адаптивной и органично отображаться на всех размерах экрана (минимальный размер экрана: 240х320, 320х240). 

Полезные ссылки:
• http://materializecss.com/
• http://bootstrap-3.ru/
• http://bootstrap-4.ru/

**Решение**: Был использован bootstrap

## Лабораторная работа 3: Генератор контента

Требуется генерировать коллажи изображений с цитатами с помощью элемента canvas с возможностью сохранения контента на чистом javascript (без html и css).

Изображения рекомендую брать отсюда: https://unsplash.com/explore/collections
Например: https://source.unsplash.com/collection/1127163/300x200

Цитаты отсюда: https://forismatic.com/ru/api/

Нужно решить задачу автоматического разбиения текста на строки для их органичного отображения.

Норм гайд по canvas: https://habrahabr.ru/post/111308/

Все необходимые материалы по js можно найти на https://learn.javascript.ru/

Для HTTP запросов можно использовать XMLHttpRequest или jQuery:
• https://learn.javascript.ru/ajax-xmlhttprequest
• http://api.jquery.com/jquery.ajax/

**Решение**: для запросов был использован JQuery, также была попытка использовать для решения __Promise__

## Лабораторная работа 4: Новостной портал

Нужно разработать новостной портал на PHP (на чистом или с фреймворками) с админкой, из которой можно добавлять и удалять новости (без авторизации). Новость должна иметь заголовок и сам текст. Храниться всё должно в MySQL.

Можно зарегистрироваться на beget (https://beget.com), там есть бесплатный домен третьего уровня. Вообще, рекомендую арендовать простой сервер и побаловаться с ним. Сам я начинал с firstvds (https://firstvds.ru), сейчас очень радует selectel (https://selectel.ru/) и его дочка vscale (https://vscale.io/). Также, можно использовать azure (https://azure.microsoft.com), там студентам можно бесплатно попользоваться небольшим облачком.

Ссылки:
• PHP tutorial https://www.w3schools.com/php/
• Yii2 https://www.yiiframework.com/doc/guide/2.0/ru
• Laravel https://laravel.ru/
• MySQL https://www.mysql.com/

**Решение**: Написано на фреймворке Laravel. Для локального развертывания использовался OpenServer.

## Лабораторная работа 5: Markdown Editor

Markdown (маркдаун) — облегчённый язык разметки, созданный с целью написания максимально читаемого и удобного для правки текста, но пригодного для преобразования в языки для продвинутых публикаций (https://ru.wikipedia.org/wiki/Markdown). Также, md вовсю используется для описания репозиториев кода, и его понимают чаты типа slack.
В данной задаче нужно сделать сервис аналогичный https://dillinger.io/

Нужно реализовать функциональность просмотра и редактирования markdown документов с возможностью хранения их на сервере. То есть обычный веб-сервис без авторизации с общим списком всех md документов. Фронт на vue, бэк на ноде (expressjs), хранить документы в монге. Для пользователей виден общий пул документов, которые они могут добавлять, менять и удалять.

Ссылки:
• Vuejs https://ru.vuejs.org/index.html
• Expressjs https://expressjs.com/ru/
• MongoDB https://www.mongodb.com/

Вот официальный пример от разработчиков Vue: https://jsfiddle.net/chrisvfritz/0dzvcf4d

Задача состоит в изучении зоопарка технологий современной web-разработки. Кода, как такового, много быть не должно.

Для получения максимального количества баллов обязательно использовать: es6, babel, webpack.

**Решение:** Создание шаблона через vue-cli решает большую часть проблем т.к. уже умеет в babel/webpack.