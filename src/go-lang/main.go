package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"

	year2024 "advent-of-code/2024"
	year2025 "advent-of-code/2025"
)

func main() {
	Run()
}

func Run() {
	// Clear screen
	fmt.Print("\033[2J\033[H")
	fmt.Println("🎅 Advent of Code 🎅")
	fmt.Println()
	fmt.Println("Select Year:")
	fmt.Println("1 : 2024")
	fmt.Println("2 : 2025")
	fmt.Println("3 : Exit")
	fmt.Print("> ")

	reader := bufio.NewReader(os.Stdin)
	input, err := reader.ReadString('\n')
	if err != nil {
		fmt.Printf("\n❌ Error reading input: %v\n", err)
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		Run()
		return
	}

	input = strings.TrimSpace(input)
	yearChoice, err := strconv.Atoi(input)
	if err != nil {
		fmt.Print("\033[2J\033[H")
		fmt.Printf("\n❌ Invalid input: %s\n", err.Error())
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		Run()
		return
	}

	switch yearChoice {
	case 1:
		year2024.RunYear2024()
		Run() // Return to year selection after exiting 2024 menu
	case 2:
		year2025.RunYear2025()
		Run() // Return to year selection after exiting 2025 menu
	case 3:
		fmt.Println("Goodbye!")
		os.Exit(0)
	default:
		fmt.Print("\033[2J\033[H")
		fmt.Printf("\n❌ Invalid selection: %d\n", yearChoice)
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		Run()
	}
}
