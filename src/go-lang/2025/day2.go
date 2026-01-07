package year2025

import (
	"advent-of-code/utils"
	"fmt"
	"strings"
)

func Day2() {
	data := utils.ReadInputFile("2025", "day2")
	cleanLines := strings.Split(strings.TrimSpace(string(data)), "\n")
	fmt.Println(cleanLines)
}
