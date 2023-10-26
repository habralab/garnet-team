Функция:
Я, как пользователь
Хочу иметь возможность посмотреть количество пользователей-участников и количество проектов в командах-участниках в составе просматриваемого проекта
Чтобы оценить масштаб проекта и силу команд, участвующих в нем

    Контекст:
        Допустим существует проект 'FooBar'
        И существует команда 'DreamTeam'
        И команда 'DreamTeam' является участником проекта 'FooBar'

    Сценарий: Отображение количества участников команды при получении списка команд-участников проекта
        Допустим в команде 'DreamTeam' количество участников равно '2'
        Когда происходит получение списка команд участников проекта 'FooBar'
        Тогда количество участников в первой команде списка равно '2'

    Сценарий: Отображение количества проектов, в которых участвует команда, при получении списка команд-участников проекта
        Допустим в команде 'DreamTeam' количество проектов равно '1'
        Когда происходит получение списка команд участников проекта 'FooBar'
        Тогда количество проектов в первой команде списка равно '1'