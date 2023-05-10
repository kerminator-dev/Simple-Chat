# Chat - приложение для обмена сообщениями.
## <a href="https://github.com/kerminator-dev/Simple-Chat/blob/master/README.md">ENG</a> | RU

![alt text](https://github.com/kerminator-dev/Simple-Chat/blob/master/img/wpf-client.png?raw=true)

Данное приложение разрабатывается с целью улучшения знаний по созданию .NET WEB-приложений с помощью WEB API и может иметь много недостатков. Я буду благодарен за новые предложения, которые улучшат код.

Основным и общим принципом является простой текстовый обмен сообщениями с другими пользователями с возможностью шифрования сообщений на стороне клиента.

### Основные функции:
- Регистрация/Вход/Выход;
- Прием и отправка сообщений в режиме реального времени от/к другим пользователям;
- Получение уведомлений в режиме реального времени (контакт вошел в систему/вышел из системы);
- Иметь синхронизированный доступ к своим контактам;
- Есть несколько устройств с различными клиентскими приложениями, подключенных к одному аккаунту.

### Текущий стек бэк-энда:
- ASP .NET 7 Web Api
- Json Web Tokens
- Entity Framework
- SQLite
- SignalR
- BCrypt

### Текущий стек клиентской стороны:
- MVVM
- WPF

# 🚩 Сделать: бэк-энд и клиентские приложения:
## Бэк-энд:
✅ Аутентификация:
- ✅ Регистрация
- ✅ Вход
- ✅ Обновить токен

✅ Сообщения:
- ✅ Отправить сообщение пользователю через SignalR-hub + real-time уведомление

✅ Пользователи:
- ✅ Получить информацию о пользователе по имени пользователя

✅ Аккаунт:
- ✅ Удалить аккаунт

✅ Контакты:
- ✅ Добавить контакт (+ real-time уведомление)
- ✅ Удалить контакты (+ real-time уведомление)
- ✅ Получить все контакты

✅ Крошечные уведомления:
- ✅ Уведомление о том, что пользователь в сети/вышел из сети


## Клиентская сторона:
- ✅ <a href="https://github.com/kerminator-dev/Simple-Chat/tree/master/src/Chat/Chat.ConsoleClientListener">Консольное клиентское приложение<a/> от <a href="https://github.com/kerminator-dev">kerminator-dev</a>
- 🚧 <a href="https://github.com/kerminator-dev/Simple-Chat/tree/master/src/Chat/Chat.Client.WPF">MVVM .NET WPF клиентское приложение<a/> от <a href="https://github.com/kerminator-dev">kerminator-dev</a>
- 🚧 <a href="https://github.com/ertanfird/simplify">Одностраничное веб-приложение на React</a> от <a href="https://github.com/ertanfird">Ertanfird</a>

## Рефакторинг:
- ❌ Пересмотреть модели базы данных
- ❌ Пересмотреть семантику кода
- ✅ Пересмотреть обработку исключений

## Тестирование:
Может быть позже...

## API-методы:
![alt text](https://github.com/kerminator-dev/Simple-Chat/blob/master/img/webAPI-methods.png?raw=true)

