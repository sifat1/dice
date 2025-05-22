# Dice Project

This project is a C# application that simulates rolling dice. It is designed to demonstrate basic programming concepts and provide a fun way to experiment with random number generation.

## Features

- Simulates rolling 3 or  more dice.
- Supports 6-sided dice (e.g., 6-sided).
- Displays the result of each roll.
- Simple and easy-to-use interface.

## Requirements

- .NET SDK (version 6.0 or later)

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/dice.git
    ```
2. Navigate to the project directory:
    ```bash
    cd dice
    ```

## Usage

1. Build the project:
    ```bash
    dotnet build
    ```
2. Run the application:
    ```bash
    dotnet run -- "2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7"
    ```

## Demo Play 
dotnet run dice.dill "2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7"
Let's determine who makes the first move.
I selected a random value in range 0..1 (HMAC=EED3DE6BAC9529708750C47A14C6B3A4EC61091B8EFEE43212663E2D348FF81C).
Try to guess my selection:
0 - 0
1 - 1
X - exit
? - help
Your selection: ?
Probability of the win fоr the user:
+ ---- ---- - + ----------- + ----------- + ----------- +
| User dice v | 2,2,4,4,9,9 | 1,1,6,6,8,8 | 3,3,5,5,7,7 |
+ ---- ---- - + ----------- + ----------- + ----------- +
| 2,2,4,4,9,9 | —           | 55.6%       | 44.4%       |
+ ---- ---- - + ----------- + ----------- + ----------- +
| 1,1,6,6,8,8 | 44.4%       | —           | 55.6%       |
+ ---- ---- - + ----------- + ----------- + ----------- +
| 3,3,5,5,7,7 | 55.6%       | 44.4%       | —           |
+ ---- ---- - + ----------- + ----------- + ----------- +

Your selection: 1
My selection: 1 (KEY=396829F6AF406D1A43EB8CB2F7FEF44C8BF159DE4BE49C82E3F94523DB39FAD2).
You guessed correctly! You make the first move.

Available Player dice:
0 - [[2, 2, 4, 4, 9, 9]]
1 - [[1, 1, 6, 6, 8, 8]]
2 - [[3, 3, 5, 5, 7, 7]]
Your selection: 0
Player chooses [2,2,4,4,9,9].
Computer chooses [3,3,5,5,7,7].

It's time for your roll.
I selected a random value in range 0..5 (HMAC=8B9AF4758241E4D24BEB9FC4A99CCB60B93F6F28C5745C94504581FCAA0C1C15).
Try to guess my selection:
0 - 0
1 - 1
2 - 2
3 - 3
4 - 4
5 - 5
X - exit
? - help
Your selection: 2
My selection: 4 (KEY=F98D1025B547B41DE0E536972292B355EBD78E9BAA8ED83211AC503FB6D0212A).
The fair number generation result is 4 + 2 = 0 (mod 6).
Your roll result is 2.

It's time for my roll.
I selected a random value in range 0..5 (HMAC=850F60FB5B7BC104DFB0BEC1B66961999915F1A9C926D15B4A6F6FCCDBA75B34).
Try to guess my selection:
0 - 0
1 - 1
2 - 2
3 - 3
4 - 4
5 - 5
X - exit
? - help
Your selection: 5
My selection: 1 (KEY=D8D7E2677D3F285367D33161E849C27CF8994CBEE9D9BAC1B9DEAB3D3CA259B4).
The fair number generation result is 1 + 5 = 0 (mod 6).
My roll result is 3.
I win 2 < 3!

