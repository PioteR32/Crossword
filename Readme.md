# Crossword Generator Engine (.NET MAUI)

A sophisticated, algorithm-driven crossword generation application built with **.NET 9** and **MAUI**. This project implements complex pattern-matching algorithms to transform structured educational data into interactive, visual crossword puzzles.

## 🧠 Algorithmic Core

The heart of this application is a custom **Crossword Generation Engine**. Unlike simple static puzzles, this engine uses a data-driven approach to:
*   **Constraint-Based Placement:** Algorithmic positioning of words in a 2D grid while maintaining intersection requirements.
*   **Topic-Based Generation:** Dynamic construction of puzzles based on user-defined topics and exercises.
*   **Validation Engine:** Complex logic to ensure that word intersections are valid and that the resulting grid is solvable.

## 🛠 Tech Stack

*   **Framework:** .NET 9 MAUI (Cross-platform: Android, iOS, Windows, MacCatalyst)
*   **Architecture:** MVVM (via `CommunityToolkit.Mvvm`)
*   **Testing:** xUnit (Advanced Data-Driven Testing)
*   **Data Layer:** Entity Framework Core (for structured data persistence)
*   **Reporting:** QuestPDF (for professional document generation)

## 🧪 Engineering Excellence: Testing Strategy

This project is built with a "Test-First" mentality. The `CrosswordTests` suite provides high-confidence verification of the core logic through:

*   **Data-Driven Testing (DDT):** Extensive use of `[Theory]` and `[MemberData]` to validate the generation algorithm against hundreds of permutations, including edge cases and invalid data sets.
*   **Regression Testing:** Ensuring that complex grid-intersection logic remains stable through continuous integration.
*   **Boundary Analysis:** Comprehensive testing of object equality, string parsing, and collection management within the `src` domain.

## 🏗 Architecture & Patterns

*   **MVVM Pattern:** Strict separation between the UI layer (`Views/Pages`) and the business logic (`src/`).
*   **Dependency Injection:** Leverages built-in .NET DI containers for service management.
*   **Custom Controls:** Reusable, highly decoupled UI components (e.g., `LetterView`, `TopicView`) to maintain a DRY (Don't Repeat Yourself) codebase.

## 🚀 Getting Started

### Prerequisites
*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   Visual Studio 2022 or VS Code

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/Crossword.git
