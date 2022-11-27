# Уеб приложение за управление на автомобилен сервиз

Предварително сийднати данни за Управител:  Имейл: "admin@crs.com"; Парола: "admin12";

Предварително сийднати данни за марка автомобили - списък от всички автомобилни марки при добавяне на заявки.


1. Потребители
a. Имена *
b. Имейл *
c. Парола *
d. Роля * - Управител или Механик
2. Заявки
a. Номер - авт.генериран, без опция за редакция
b. Дата и час - авт.генерирана, с опция за редакция
c. Автомобил * - избор от списък
d. Описание на проблема * - от 5 до 300 символа
3. Автомобили
a. Марка *
b. Цвят *
c. Регистрационен номер
Страници
1. Вход
a. Имейл и Парола
2. Потребители
a. Добавяне/редакция/изтриване
b. Достъп да имат само Управителите
3. Заявки
a. Добавяне/редакция/изтриване
b. При добавяне или редакция, да може да се добавя или избира вече съществуващ
автомобил. Добавянето е със съответните му параметри.
4. Справка
a. Единично текстово поле, което да търси измежду параметрите на всяка заявка и
автомобилите към нея.
b. Резултатите да съдържат “Заявка №”, “Дата и час”, “Данни за Автомобила”,
“Данни за Потребителя (без парола)”, “Описание на проблема”
c. При визуализирането на резултатите, фразата която е потърсена да бъде
акцентирана при резултатите (highlight)


Управители:
(по подразбиране да има управител със статични данни за вход)
a. Потребители - Позволен достъп
b. Заявки - Позволен достъп, Виждат всички заявки
c. Справка - Позволен достъп

Механици:
a. Потребители - Забранен достъп
b. Заявки - Позволен достъп, Виждат само техните заявки
c. Справка - Позволен достъ
