# Physical Source

## Краткое описание функционала

## **class TwoDimesional**
Класс, которых хранит двумерную координату.

### **Поля:**
- *double* **X** - X-координата
- *double* **Y** - Y-координата (внезапно)

### **Методы:**
- **TwoDimesional** (*double*, *double*) - Конструктор
- *TwoDimesional* **operator+** (*TwoDimesional*, *TwoDimesional*) - оператор сложения двух экземпляров класса
- *TwoDimesional* **operator*** (*TwoDimesional*, *double*) - оператор умножения на коефициент



## **class Tool**
Класс с дополнительными статическими методами.

### **Доступные методы:**
- *double* **Distance** (*TwoDimesional*, *TwoDimesional*) - возвращает расстояние между точками
- *Ellipse* **GenerateEllipse** (*double*) - создает шаблонный **Ellipse** с заднанным размером



## **abstract class PhysicalBaseObject**
Класс, который реализует физический объект.

### **Методы для определения / переопределения:**
- abstract *void* **CustomConduct**() - метод, в котором нужно определить поведение объекта

### **Поля:**
- readonly *PhysicalField* **Field** - поле, к которому привязан объект
- readonly *Ellipse* **MatherialObject** - графичский объект, который описывает класс
- *TwoDimesional* **Position** - относительные координаты объекта на графическом поле

### **Методы:**
- **PhysicalBaseObject**(*PhysicalField*, *double*, *TwoDimesional*, *TwoDimesional*) - конструктор, принимает:
-- физическое поле к которому привязан объект
-- размер круга (эллипса)
-- изначальное расположение объекта
--  вектор начальной скорости
- *void* **StopMoving**() - задает нулевую скорость объекту
- *void* **UpdateMoveDirection**(*int*) - изменяет вектор перемещения в зависимости от заданного переопределенного метода **CustomConduct** [Автоматически выполняется полем]

### **Свойства:**
- *TwoDimesional* **Size** - возвращает/задает размер фигуры	
- *TwoDimesional* **AccelerationDirection** - возвращает/задает вектор ускорения
- *TwoDimesional* **MoveDirection** - задает новый вектор движения / возвращает текущий вектор движения



## **class PhysicalField**
Класс, который реализует физическое пространство, связывает и реализует перемещение объектов класса **PhysicalBaseObject** (и производных от него).

### **Поля:**
- readonly *List<PhysicalBaseObject>* **PhysicalBaseObjects** - хранит все объекты добавленные на поле, которые не являются статическими
- readonly *List<**PhysicalBaseObject>* **StaticObjects** - хранит все статические объекты
- readonly *Canvas* **FieldCanvas** - хранит Canvas к которому привязано поле

### **Методы:**
- **PhysicalField**(*Canvas*, *int*) - конструктор, который принимает *Canvas* на котором будут размещаться объекты и периодичность, с которой будет обновляться.
- *void* **AddObject**(*PhysicalBaseObject*) - добавляет новый объект на поле
- *void* **AddStaticObject**(*PhysicalBaseObject*) - добавляет новый объект на поле **для которого не будет вызывается метод перемещения**
- *void* **Start**(*object*, *EventArgs*) - запускает движение объектов. (Входные параметры для соответствия EventHandler'у, чтобы можно было назначить на нажатие кнопки)
