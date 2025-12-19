package year2025

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func MenuItems() map[int]func() {
	return map[int]func(){
		// Add days here as they're implemented
		// 1: Day1,
	}
}

func RunYear2025() {
	menuItems := MenuItems()

	// Clear screen (ANSI escape code)
	fmt.Print("\033[2J\033[H")
	fmt.Println("🎅Advent of Code 2025🎅")
	fmt.Println()

	if len(menuItems) == 0 {
		fmt.Println("No days implemented yet!")
		fmt.Println()
		fmt.Println("1 : Back to Year Selection")
		fmt.Print("> ")

		reader := bufio.NewReader(os.Stdin)
		reader.ReadString('\n')
		return
	}

	// Display menu items
	for i := 1; i <= len(menuItems); i++ {
		if _, exists := menuItems[i]; exists {
			fmt.Println(i, ":", "Day ", i)
		}
	}
	fmt.Println(len(menuItems)+1, ":", "Back to Year Selection")
	fmt.Print("> ")

	// Read user input
	reader := bufio.NewReader(os.Stdin)
	input, err := reader.ReadString('\n')
	if err != nil {
		fmt.Printf("\n❌ Error reading input: %v\n", err)
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		RunYear2025()
		return
	}

	// Parse input
	input = strings.TrimSpace(input)
	key, err := strconv.Atoi(input)
	if err != nil {
		fmt.Print("\033[2J\033[H")
		fmt.Printf("\n❌ Invalid input: %s\n", err.Error())
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		RunYear2025()
		return
	}

	// Handle back to year selection
	if key == len(menuItems)+1 {
		return // Returns to main menu
	}

	// Execute selected day
	if dayFunc, exists := menuItems[key]; exists {
		fmt.Print("\033[2J\033[H")
		fmt.Printf("🎅Advent of Code 2025🎅%sDay %d%s", "\n\n", key, "\n\n")
		dayFunc()
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		RunYear2025()
	} else {
		fmt.Print("\033[2J\033[H")
		fmt.Printf("\n❌ Invalid selection: %d\n", key)
		fmt.Print("Press Enter to continue...")
		reader.ReadString('\n')
		RunYear2025()
	}
}
