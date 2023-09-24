Функция: 
Я, как пользователь 
Хочу иметь возможность искать других пользователей по определенным фильтрам
Чтобы в последствии приглашать их в команды

    Сценарий: Полнотекстовый поиск пользователей
        Допустим существует пользователь 'Василий Петров'
        И существует пользователь 'Мария Алексеевна'
        И существует пользователь 'Геннадий Сергеевич'
        Когда производится поиск пользователей с запросом 'Мария'
        Тогда в списке отображается '1' пользователь
        И в списке у первого пользователя в имени присутсвтует 'Мария'

    Сценарий: Поиск пользователей по навыкам
        Допустим существует пользователь 'Вася' с тегами 'sql, c++'
        И существует пользователь 'Маша' с тегами 'figma, photoshop'
        Когда производится поиск пользователей по тегам 'sql'
        Тогда в списке отображается '1' пользователь
        И в списке у первого пользователя в навыках присутствует 'sql'