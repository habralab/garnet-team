﻿Функция:
Я, как пользователь-владелец команды
Хочу иметь возможность подать заявку на вступление в проект от лица своей команды
Чтобы поучаствовать в реализации идеи проекта

    Сценарий: Успешная отправка заявки на вступление в проект
        Допустим существует пользователь 'Вася'
        И существует команда 'DreamTeam'
        И пользователь 'Вася' является владельцем команды 'DreamTeam'
        И существует проект 'FooBar'
        Когда пользователь 'Вася' отправляет заявку на вступление в проект 'FooBar' от лица команды 'DreamTeam'
        Тогда в проекте 'FooBar' существует заявка на вступление от команды 'DreamTeam'