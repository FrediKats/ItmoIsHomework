# Physical Source

Идет очень активная разработка. Вопросы/Таски приветствуются.

# Краткий дескрипшен

## **class TwoDimesional**
Класс, которых хранит двумерную координату.

### **Свойства:**
- **X** - получение X-координаты
- **Y** - получение Y-координаты (внезапно)

## **class Tool**
Класс с дополнительными статическими методами.

### **Доступные методы:**
- double **Distance**(**TwoDimesional** *f*, **TwoDimesional** *s*) - возвращает расстояние по Пифагору


## **class PhysicalObject**
Класс, который реализует физический объект.

### **Методы для определения / переопределения:**
- abstract void **CustomConduct**() - метод, в котором нужно определить поведение объекта

### **Доступные методы:**
- **PhysicalObject**(**PhysicalField** *field*, **TwoDimesional** *size*, **TwoDimesional** *position*, **TwoDimesional** *moveDirection*) - конструктор, принимает:
-- field - физическое поле к которому привязан объект
-- size - размер круга (эллипса)
-- position - изначальное расположение объекта
-- moveDirection - вектор начальной скорости
- void **StopMoving**() - задает нулевую скорость объекту
- void **UpdateMoveDirection**(**int** *timePassed*) - изменяет вектор перемещения в зависимости от заданного переопределенного метода **SetAccelerationVector**
- void **MoveObject**(**int** *timePassed*) - перемещает объект на вектор скорости

### **Свойства:**
- Ellipse **ObjectEllipse** - возвращает фигуру
- TwoDimesional **Position** - возвращает текущую позицию фигуры
- PhysicalField **Field** - возвращает поле, к которому привязае объект

- TwoDimesional **AccelerationDirection** - задает вектор ускорения
- TwoDimesional **MoveDirection** - задает новый вектор движения

## **class PhysicalField**
Класс, который реализует физическое пространство, связывает и реализует перемещение объектов класса **PhysicalObject** (и производных от него).

### **Доступные методы:**
- **PhysicalField**(**Canvas** *fieldCanvas*, **int** *timePerTick*) - конструктор, который принимает **Canvas** на котором будут размещаться объекты и периодичность, с которой будет обновляться.
- void **AddObject**(**PhysicalObject** *obj*) - добавляет новый объект на поле
- void **AddObject**(**TwoDimesional** *size*, **TwoDimesional** *position*, **TwoDimesional** *moveDirection*) - принимает нужные параметры для создания объекта, создает и сразу же добавляет на поле
- void **AddStaticObject**(**PhysicalObject** *obj*) - добавляет новый объект на поле **для которого не будет вызывается метод перемещения**
- void **Start**(**object** *sender*, **EventArgs** *e*) - запускает движение объектов. (Входные параметры для соответствия EventHandler делегату, чтобы можно было назначить на нажатие кнопки)

### **Свойства:**
- **List**<**PhysicalObject**> *PhysicalObjects* - возвращает все объекты добавленные на поле
- **List**<**PhysicalObject**> *StaticObjects* - возвращает все статические объекты
- **Canvas** *FieldCanvas* - возвращает Canvas к которому привязано поле


# To-do list:
- Обдумать корректность нейминга
- Написать метод изменения скорости
- Реализовать возможно построение графика по траектории перемещению
