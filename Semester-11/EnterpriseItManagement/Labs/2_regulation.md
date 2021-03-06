# 1. Описание сервиса

Для поддержки бизнес-процессов в рамках образовательной деятельности планируется развернуть новую информационную систему. Система представляет собой набор сервисов и систем хранения данных, которые должны быть подняты и быть доступными для клиентов. Пользователями системы являются студенты, преподаватели и представители администрации образовательной программы. 

Сервис поделён несколько подсервисов, которые соответствуют основным функциональным требованиям:

## Сервис управления заданиями

Сервис предоставляет возможность назначать и управлять заданиями, которые менторы выдают студентам в рамках учебного процесса, а также отслеживать выполнение и оценивание предоставляемых результатов.

### Use cases

1. Добавление пользователей и сущностей
   1. Система должна позволять добавить новый учебный предмет
   2. Система должна позволять добавить нового пользователя и назначить его ментором на предмет
      1. Должна быть возможность указать ФИО пользователя
      2. Должна быть возможность добавить ему пароль или сгенерировать случайны
   3. Система должна позволять добавить список пользователей и привязать их к группе
   4. Система должна позволять пользователю менять свой пароль
2. Добавление заданий:
   1. Система должна позволять ментору добавить новое задание, которое состоит из:
      1. Названия
      2. Описания требуемой работы
      3. Номер задания в курсе
      4. Дедлайн
   2. Система должна поддерживать добавление скрытых заданий - такие задания видны только менторам, пока его не сделают публичным
   3. Система должна позволять изменять все атрибуты добавленного задания

3. Загрузка работы
   1. Система должна иметь интерфейс студента для добавления решений. При добавлении указывается
      1. Предмет, номер работы
      2. Комментарий по выполнению работы
      3. Ссылка на репозиторий
      4. Указать путь до папки, где в репозитории лежат необходимые данные
   2. Система должна уметь по ссылке загружать данные и сохранять себе локально
   3. Допущения:
      1. Для упрощения работы с доступом, от студентов будет требоваться добавлять только те репозитории, которые находятся в организациях, к которым есть доступ из системы.
4. Проверка работы
   1. Система должна предоставлять возможность выставить оценку за выполненную работу
      1. Работа не должна быть отмечена как плагиат
      2. При выставлении можно указать комментарии
      3. Работе может быть выставлен один из двух решений: апрув или реджект
   2. Система должна держать активным только один сабмит по каждой лабораторной
      1. При загрузке второй работы по задания старое задание должно быть

## Сервис поиска плагиата

Сервис для автоматического поиска схожих решений студентов для помощи преподавателю в принятии решения о плагиате.

### Основные юз кейсы

1. Авторизация
   1. AU1 Возможность работать под кредами выданными Iwentys
      1. Я, как пользователь с учеткой в Iwentys, авторизируюсь под ней в системе
      2. Система делегирует валидацию в сервис Iwentys, убеждается, что у меня есть права
2. Интеграция
   1. IN1 Выгрузка репозиториев с гитхаба (75)
      1. Я, как авторизированный пользователь, запускаю выгрузку репозиториев, указываю организацию
      2. Система выгружает с гитхаба список всех репозиториев по данной организации
      3. Система обновляет все репозитории из списка
      4. Я вижу информацию о количестве репозиториев, которые были обновлены, и количестве, которые не изменились
   2. IN2 Прогресс трекинг выгрузки репозиториев (75)
      1. Я, как авторизированный пользователь, запускаю выгрузку репозиториев, указываю организацию
      2. Система начинает выгружать репозитории. После каждого выгруженного репозитория система уведомляет об этом
      3. В интерфейсе отображается прогресс, список выгруженных, очередь на выгрузку
   3. IN3 Последнее обновление репозитория
      1. Я, как авторизированный пользователь, запускаю выгрузку репозиториев, указываю организацию
      2. Система выгружает информацию о том, когда был создан последний комит
      3. При просмотре списка репозиториев отображается также время последнего запуска обновления и последнего комита
   4. IN4 Последнее обновление сабмита
      1. Я, как авторизированный пользователь, запускаю выгрузку репозиториев, указываю организацию
      2. Система выгружает с гитхаба информацию о том, когда был создан последний комит по определённому сабмиту
      3. При просмотре списка репозиториев отображается также время последнего запуска обновления и последнего комита по каждому сабмиту
   5. IN5 Проверка пул реквеста
      1. Я, как авторизированный пользователь, указаю пул реквест для проверки
      2. Система определяет предмет, который связан с ним, генерируется список пар для сравнений, запускает проверку
      3. После проверок система отображает список всех проверенных пар, где фигурирует этот пул реквест (репозиторий, с которым сравнивали + процент схожести с ним).
   6. IN6 История проверок пул реквестов
      1. Я, как авторизированный пользователь, открываю историю проверок пул реквестов
      2. Вижу информацию по каждому запуску проверки - дата запуска, ссылка на пул реквест, результаты
   7. IN7 Вебхук на новый пул реквест
      1. Я, как авторизированный пользователь, включаю у определённого предмета автоматическую проверку на пул реквестах
      2. Система подписывается на вебхуки и при создании нового пул реквеста запускает его проверку
      3. В списке автозапущенных проверок, я вижу все проверки, которые запускались по вебхуку
      4. Критерии принятия:
         1. Проверки должны запускаться только по тем пулреквестам, которые прошли CI проверки
         2. Система должна реагировать не только на создание пул реквеста, но и на добавление новых комитов в него
3. Управление предметами 
   1. SM1 Добавление предмета в систему (65)
      1. Я, как админ системы, запрашиваю создание нового предмета, указываю
         1. Название
         2. Ссылку на организацию
         3. Структуру файлов в репозиториях
      2. Система добавляет новый предмет
      3. Я открываю список предметов и вижу только что добавленный предмет
   2. SM2 Обновление репозиториев по предмету
      1. Я, как преподаватель, могу открыть список предметов, выбрать определённый, запросить обновление по нему
      2. Система должна убедиться, что на данный момент не выполняется обновление по нему
      3. Система должна обновить репозитории по указанной к предмету организации
      4. После того, как система закончит обновление, я должен увидеть "Last update: %date%"
4. Сравнение пар
   1. CM1 Сравнение указанных двух работ
      1. Я, как авторизированный пользователь, выбираю две работы из тех, которые уже загружены в систему, запускаю сравнение по ним
      2. Система начинает сравнивать работы, генерирует информацию о схожести
      3. Я открываю список всех сравнений, вижу там свою пару и информацию о ней:
         1. Гитхаб двух студентов
         2. Информацию о том, что за лабораторная, когда запустилась проверка
         3. Процент схожести лабораторных
   2. CM2 Верификация схожести. Не схожие
      1. Я. как авторизированный пользователь, открываю список непроверенных пар выбираю пару работ, которая уже сравнивалась, указываю что работы не схожи
      2. Система должна сохранить информацию о том, что пара проверена и отмечена как не схожая
      3. При повторном открытии списка непроверенных пар, данная пара не появляется
   3. CM3 Верификация схожести. Схожие работы
      1. Я. как авторизированный пользователь, открываю список непроверенных пар выбираю пару работ, которая уже сравнивалась, указываю что работы схожие
      2. Система должна сохранить информацию о том, что пара проверена и отмечена как схожая
      3. При повторном открытии списка непроверенных пар не отображаются пары с теми работами, которые были помечены как схожие
   4. CM4 Фильтры страницы схожести (68)
      1. Я, как авторизированный пользователь, могу открыть список непроверенных пар
      2. Я могу указать фильтры:
         1. По предмету
         2. По студенту / студентам
         3. По лабораторной
         4. По минимальному проценту схожести
      3. Я могу задать опцию "Показывать даже те работы, которые уже отмечены как списанные"
      4. Система должно отображать только те пары, которые удовлетворяют заданному фильтру
   5. CM4 Сброс верификации при обновлении
      1. Я, как пользователь, запрашиваю обновление работы, которая была уже проверена
      2. Если работа отмечена как списанная, то она не обновляется
      3. Если работа отмечена как не списанная, то она обновляется, но все сравнения с ней меняются на "Проверена не последняя версия"
      4. При просмотре списка пар я вижу, что работа обновилась после проверки
   6. CM5 Фильтр сравнения с шаблонным репозиторием
      1. Я, как пользователь системы, открываю настройки предмета
      2. Указываю там название шаблонного репозитория
      3. Система при попарном сравнении игнорирует этот репо и не сравнивает с ним
   7. CM6 Фильтр пустышек
      1. Я, как пользователь системы, открываю пары по предмету с указанным шаблонным репозиторием
      2. Система сравнивает работы с шаблонным репозиторием, определяет те работы, которые не изменились, отмечает их как "Пустые"
      3. Пустые работы не отображаются в системе, с ними не выполняется сравнение работ
5. Сопоставление данных
   1. DM1 Юзер профили
      1. Я, как авторизированный пользователь системы, открываю профиль студента
      2. Вижу там:
         1. Его юзернейм, ФИО, группы
         2. Данные о том, когда последний раз были загружены обновления по каждой лабораторной
         3. Информация о том, как пары были проверены и их вердикт
         4. Список непроверенных пар с высоким процентом схожести
   2. DM1 Референсы на юзера
      1. Я, как авторизированный пользователь системы, при просмотре пар сравнения могу выбрать любого юзера
      2. Система должна направить меня на его профиль
6. Просмотр диффа
   1. DF1 Просмотр диффа по работе
      1. Я, как авторизированный пользователь системы, открываю пару работ
      2. Вижу дифф работ

# 2. Окружение

Основной ИТ-сервис, который находится на предприятии и с которым нужно интегрироваться - это ИСУ. ИСУ - сервис, который управляет студентами, их оцениванием.

```plantuml
@startuml Iwentys_Context
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

System_Boundary(actors, "Actors") {
    Person(mentor, "Mentor")
    Person(student, "Student")
    Person(systemAdmin, "Admin")
    Person(manager, "Manager")
}

System_Boundary(externalApi, "External API") {
    System_Ext(github, "Github")
    System_Ext(googleService, "Google services")
    System_Ext(bars, "University API")
}

' Assignments
System_Boundary(iwentysContext, "Services") {
    Container(assignmentService_werServer, "Web application", "ASP Web API")
    Container(detChecker_werServer, "Web application", "ASP Web API")
    Container(reportService_werServer, "Web application", "ASP Web API")
    Container(seaInk_werServer, "Web application", "ASP Web API")
    Container(userService_werServer, "Web application", "ASP Web API")
}

' REL============================
' Assignments
Rel(mentor, assignmentService_werServer, "Invite to github organization, add tasks, points, create tables")
Rel(student, assignmentService_werServer, "Register to queue")

Rel(assignmentService_werServer, userService_werServer, "Get user info, user autorization")
Rel(assignmentService_werServer, detChecker_werServer, "Start work validation")
Rel(assignmentService_werServer, seaInk_werServer, "Create and update tables")
Rel(assignmentService_werServer, reportService_werServer, "Create reports")

Rel(assignmentService_werServer, github, "Send ivnites to organization, load user repository")

' Plagiarism
Rel(detChecker_werServer, github, "Import works")

' Report
Rel(reportService_werServer, github, "Import works")

' SeaInk
Rel(seaInk_werServer, googleService, "Create tables, parse data")
Rel(seaInk_werServer, bars, "Sync points with university system")
Rel(seaInk_werServer, userService_werServer, "Sync students and group list")


Rel(systemAdmin, seaInk_werServer, "Configure table parsing")

'User Generator
Rel(systemAdmin, userService_werServer, "Update user info")
Rel(userService_werServer, bars, "Get updated user info")

@enduml
```

Потребители сервиса:
- Студенты и менторы. Основные пользователи, которые используют сервис для решения своих задач и автоматизации действий.
- Администрация факультета. Является также продукт овнером сервиса, формируют требования, запросы на функционал, проецируют бизнес цели бизнес процессов на ожидания от сервиса.

# 3. Процессы ITIL

1. Управление инцидентами (Incident management) - Устранение инцидентов, создание подразделения Service Desk, которое является единой точкой контакта с пользователями и координируется процесс устранения.
<!-- 2. Управление проблемами (Problem management) - выявление и устранение причин инцидентов. -->
<!-- 3. Управление конфигурациями (Configuration management) - создание и поддержка инфраструктуры  -->
4. Управление изменениями (Change management) - определение допустимых и нужных изменений в системе.
5. Управление релизами (Release management) - выкатывание релизов без ущерба для производственной среды.
<!-- 6. Управление уровнем сервиса (Service Level Management) - мониторинг метрик из SLA, уточнение и изменение требований. -->
<!-- 7. Управление мощностью (Capacity management) - анализ нагрузки, определение необходимого уровня ресурсов. -->
<!-- 8.  Управление доступностью (Availability management) -  -->

# 4. Роли в рамках процессов

- Управление инцидентами
  - Представитель поддержки. Обработка запросов от клиентов, общение с ними, предоставление результатов решения инцидентов.
  - Пользователь-студент
  - Пользователь-ментор
- Управление изменениями
  - Разработчик. Вносит изменения в систему, проверяет работоспособность системы в новой конфигурации
  - Аналитик. Анализирует и согласовывает изменения, проводит информирование
  - Продукт овнер. Перестраивает свои процессы в связи с изменениями
- Управление релизами
  - Продукт овнер. Представляет интересы клиенты, формирует запросы на изменения функционала сервиса.
  - Аналитик. Анализ запросов клиентов, приоритизация, формирование беклога задач.
  - Разработчик. Расширение функционала сервиса, устранение проблем.

# 5. Регламент

- Предоставление поддержки при инциденте:
  - Пользователь-студент или пользователь-ментор обращается письмом на почту поддержки
  - Представитель поддержки в течении времени указанного в SLA должен обработать заявку, определить приоритеты, уведомить пользователя об эстимейтах по решению инцидента
  - Представитель поддержки мониторит процесс, непрерывно обновляет стейт и информирует об изменениях.
  - По результатам решения получает подтверждение от пользователя о том, что инцидент урегулирован.
- Управление изменениями
  - Разработчик формирует объем изменений в API, в процессах взаимодействия с АПИ внешних сервисов и в сценариях работы пользователей с системой
  - Аналитик проводит анализ и согласование изменений, утверждает их с продукт овнером
  - Реализует необходимое информирование
- Управление релизами:
  - Продукт овнер анализирует работу сервиса и формирует запрос на добавление функционала или изменение текущего. Обращения оформляются по предоставляемому шаблону и направляются на почту аналитику.
  - Аналитик проверяет полученные запросы, валидирует её, приоритезирует. Приоритетные обращения переводятся в задачи и отправляются в беклог разработки.
  - Разработчик обрабатывает задачи с беклога, реализует необходимый функционал. После реализации формирует документацию для аналитиков и поддержки с целью донесения информации о внесённых изменения в систему.
  - Аналитик предоставляет информацию о расширении функционала конечному потребителю