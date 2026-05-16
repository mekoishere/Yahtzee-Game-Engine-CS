# Yahtzee Game – .NET Implementation

## [PL] Opis Projektu
Projekt jest implementacją klasycznej gry w kości dla 2-4 graczy, napisaną w języku C# (.NET 9.0). Aplikacja została podzielona na trzy logiczne warstwy (projekty), co zapewnia separację odpowiedzialności oraz ułatwia testowanie i rozbudowę.

### Decyzje projektowe i funkcjonalne
1. **Podział na projekty (Clean Architecture):**
   - `Yahtzee.Logic`: Zawiera silnik gry, bezstanowy kalkulator punktów oraz logikę arkusza wyników. Nie posiada zależności od UI.
   - `Yahtzee.Console`: Warstwa prezentacji. Wykorzystuje ASCII Art do wyświetlania kości i zapewnia interakcję z użytkownikiem.
   - `Yahtzee.Tests`: Zestaw testów jednostkowych (NUnit) pokrywających 100% krytycznej logiki punktowania.
2. **Bezstanowy Kalkulator (`ScoreCalculator`):** Silnik obliczający punkty jest czystą funkcją. Przyjmuje układ kości i kategorię, zwracając wynik. Ułatwia to testowanie i sprawia, że logika jest przewidywalna.
3. **Enkapsulacja Arkusza (`ScoreSheet`):** Arkusz sam dba o walidację (nie pozwala zapisać wyniku dwa razy w to samo miejsce) oraz automatycznie wylicza sumy i bonusy.
4. **Język projektu:** Ponieważ kwestia językowa nie została precyzyjnie określona w specyfikacji zadania (`zadanie.pdf`), cały kod źródłowy oraz interfejs użytkownika zostały napisane w języku angielskim. Jest to powszechny standard branżowy, który zapewnia spójność z terminologią języka C# i ułatwia czytanie kodu. Wizualną atrakcyjność konsoli podnoszą dodatkowo elementy ASCII Art.

### Instrukcja uruchomienia
Wymagane środowisko: .NET 9.0 SDK.

1. **Uruchomienie gry:**
   dotnet run --project Yahtzee.Console

2. **Uruchomienie testów:**
   dotnet test

---

## [EN] Project Description
This project is a C# (.NET 9.0) implementation of the classic Yahtzee dice game for 2-4 players. The solution is architected into three distinct projects to ensure separation of concerns and maintainability.

### Design and Functional Decisions
1. **Separation of Concerns:**
   - `Yahtzee.Logic`: Core engine, stateless point calculator, and score sheet logic.
   - `Yahtzee.Console`: UI layer using ASCII Art for dice rendering and console interaction.
   - `Yahtzee.Tests`: Unit test suite (NUnit) covering 100% of the scoring logic.
2. **Stateless Calculator:** The scoring engine is implemented as a pure logic provider. It takes a dice set and a category as input and returns the score, making the logic predictable and easy to test.
3. **ScoreSheet Encapsulation:** The `ScoreSheet` class manages validation (preventing double entries) and automatically calculates the upper section bonus and grand totals.
4. **Project Language:** Since the preferred language was not explicitly specified in the requirements (`zadanie.pdf`), the entire codebase and user interface are implemented in English. This adheres to industry standards, maintains consistency with internal .NET naming conventions, and improves code readability. ASCII Art is utilized to enhance the terminal-based user experience.

### How to Run
Requirement: .NET 9.0 SDK.

1. **Run the Game:**
   dotnet run --project Yahtzee.Console

2. **Run the Tests:**
   dotnet test
