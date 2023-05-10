# Chat - full stack chat application.
## ENG | <a href="https://github.com/kerminator-dev/Simple-Chat/blob/master/README-RU.md">RU</a>

![alt text](https://github.com/kerminator-dev/Simple-Chat/blob/master/img/wpf-client.png?raw=true)

This project was made to improve knowlegde of building a .NET WEB applications with WEB Api and also can have many flaws. I would be appreciate for a new suggestions to improve the code. 

The basic and general principle is simple text messaging to another users with ability to encrypt messages on client-sides.
 
 ### Main functionality:
 - Register/Login/Logout
 - Receive and send in real-time text messages from/to another users
 - Receive in real-time notifications (contact gets online/offline)
 - Have multiple devices with different client apps, connected to one account. 
 
### Current back-end stack:
- ASP .NET 7 Web Api
- Json Web Tokens
- Entity Framework
- SQLite
- SignalR
- BCrypt

### Current client-side stack:
- MVVM
- WPF

# 🚩 To do: back-end and client apps:
## Back-end:
✅ Authentication:
- ✅ Registration
- ✅ Login
- ✅ Refresh token

✅ Messages:
- ✅ Send message to user throw SignalR-hub, real-time SignalR notifications 

✅ Users:
- ✅ Get user's info by username

✅ Account:
- ✅ Delete account

✅ Contacts:
- ✅ Add contacts + real-time SignalR notification
- ✅ Delete contacts + real-time SignalR notification
- ✅ Get all contacts

✅ Tiny notifications:
- ✅ User gets online/offline


## Client side:
- ✅ <a href="https://github.com/kerminator-dev/Simple-Chat/tree/master/src/Chat/Chat.ConsoleClientListener">Console client-listener app<a/> by <a href="https://github.com/kerminator-dev">kerminator-dev</a>
- 🚧 <a href="https://github.com/kerminator-dev/Simple-Chat/tree/master/src/Chat/Chat.Client.WPF">MVVM .NET WPF client app<a/> by <a href="https://github.com/kerminator-dev">kerminator-dev</a>
- 🚧 <a href="https://github.com/ertanfird/simplify">Single page React web app</a> by <a href="https://github.com/ertanfird">Ertanfird</a>

## Refactoring:
- ❌ Review database models
- ❌ Review code semantics
- ✅ Review exception handling

## Testing
May be later...

## Allowed API-endpoints:
![alt text](https://github.com/kerminator-dev/Simple-Chat/blob/master/img/webAPI-methods.png?raw=true)
