# Anserem Email Filter

Веб-приложение для обработки email-писем по бизнес-правилам.

Реализовано в рамках тестового задания на позицию .NET & JS разработчика в компанию **Anserem**.

---

## Функциональность

- Обработка полей `To` и `Copy` по списку доменов, исключений и подстановок
- Удаление подстановок при наличии исключений
- Подстановка email-адресов при выполнении условий
- Валидация email-адресов (отсекаются некорректные и подозрительные адреса)
- Веб-интерфейс + REST API
- Покрыто юнит-тестами (xUnit)

---

## Технологии

**Бэкенд**:
- ASP.NET Core 8
- C#
- xUnit (тестирование)

**Фронтенд**:
- HTML + JavaScript
- LESS (скомпилирован в CSS)

---

## Как запустить

```bash
git clone https://github.com/sayumikko/AnseremEmailFilter.git
cd AnseremEmailFilter
dotnet restore
dotnet run
```

После запуска перейдите на:
```
http://localhost:5080
```

## Тестирование

Из корневой папки проекта:
```bash
cd AnseremEmailFilter.Tests
dotnet test
```
