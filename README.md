# Dokumentacja Systemu Naprawy Samochodów

> System Naprawy Samochodów stworzony w technologii ASP.NET Core z wykorzystaniem Azure. Aplikacja umożliwia dodawanie informacji o samochodach, przeglądanie szczegółów samochodów oraz zarządzanie raportami uszkodzeń.

## Spis Treści
* [Informacje Ogólne](#informacje-ogólne)
* [Technologie Użyte](#technologie-użyte)
* [Funkcjonalności](#funkcjonalności)
* [Screenshots](#Screenshots)
* [Instalacja](#instalacja)
* [Jak Korzystać](#jak-korzystać)
* [Status Projektu](#status-projektu)
* [Plan Rozwoju](#plan-rozwoju)
* [Wzorce Projektowe](#wzorce-projektowe)
* [Twórcy](#twórcy)
* [Kontakt](#kontakt)

## Informacje Ogólne
- Celem aplikacji jest ułatwienie procesu zarządzania naprawami samochodów poprzez intuicyjny interfejs użytkownika.
- Umożliwia użytkownikom zarządzanie informacjami o samochodach oraz raportami uszkodzeń.
- Aplikacja zawiera funkcjonalności takie jak dodawanie, edytowanie, przeglądanie i zarządzanie raportami uszkodzeń.

## Technologie Użyte
- ASP.NET Core
- Entity Framework Core
- Azure Blob Storage
- Azure SQL Database
- Visual Studio

## Funkcjonalności
- Dodawanie informacji o samochodach, w tym: nazwa, model, rok, typ (elektryczny lub spalinowy), pojemność baterii lub paliwa, zdjęcia i raporty uszkodzeń.
- Edycja informacji o samochodach.
- Przeglądanie szczegółów samochodów.
- Zarządzanie raportami uszkodzeń.
- Asynchroniczne przesyłanie plików do Azure Blob Storage.

## Screenshots

**Strona główna**
![Strona główna](Screenshots/stronaglowna.png)

**Rejestracja**
![Rejestracja](Screenshots/rejestracja.png)

**Logowanie**
![Logowanie](Screenshots/logowanie.png)

**Panel Użytkownika**
![Panel Użytkownika](Screenshots/paneluzytkownika.png)


## Instalacja
Aby uruchomić projekt lokalnie:
1. Sklonuj repozytorium.
2. Skonfiguruj połączenie z bazą danych Azure SQL w `appsettings.json`.
3. Skonfiguruj połączenie z Azure Blob Storage w `appsettings.json`.
4. Otwórz projekt w Visual Studio.
5. Uruchom migracje baz danych za pomocą Entity Framework Core.
6. Uruchom projekt.

## Jak Korzystać
- Dodawaj, edytuj i przeglądaj informacje o samochodach.
- Zarządzaj raportami uszkodzeń samochodów.

## Status Projektu
Projekt jest w trakcie rozwoju.

## Plan Rozwoju
- Dodanie funkcji usuwania samochodów.
- Poprawa i zmiana wyglądu interfejsu użytkownika.
- Dodanie bardziej zaawansowanych funkcji zarządzania naprawami.

## Wzorce Projektowe

- **Repository**: Oddzielenie logiki dostępu do danych od logiki biznesowej.
- **Factory**: Tworzenie instancji obiektów bez bezpośredniego użycia operatora `new`.
- **EventHandler**: Implementacja logiki obsługi zdarzeń w odpowiedzi na akcje.
- **Blob**: Przechowywanie plików (zdjęć i raportów) w Azure Blob Storage.
- **Interfaces**: Definiowanie kontraktów dla komponentów aplikacji, co ułatwia testowanie i zarządzanie kodem.

## Twórcy
- Projekt stworzony przez [@Pawgni](https://github.com/Pawgni).

## Kontakt
- W przypadku pytań lub sugestii, proszę kontaktować się z twórcą projektu.
