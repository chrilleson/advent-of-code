package year2024

import (
	"fmt"
	"regexp"
	"strconv"
	"strings"

	"advent-of-code/utils"
)

func Day3() {
	data := utils.ReadInputFile("2024", "day3")
	lines := strings.Split(strings.TrimSpace(string(data)), "\n")

	sum := 0
	for _, v := range mulCalculator(lines) {
		sum += v
	}

	fmt.Printf("Sum of mul: %d\n", sum)
}

func mulCalculator(lines []string) []int {
	mulRegex := regexp.MustCompile(`mul\(\d{1,3},\d{1,3}\)`)
	matchRegex := regexp.MustCompile(`do\(\)|don't\(\)|mul\(\d{1,3},\d{1,3}\)`)

	joined := strings.Join(lines, "")
	matches := matchRegex.FindAllString(joined, -1)

	var results []int
	currentOperator := ""

	for _, match := range matches {
		switch match {
		case "do()":
			currentOperator = "do"
		case "don't()":
			currentOperator = "don't"
		default:
			if currentOperator == "don't" || (currentOperator == "do" && !mulRegex.MatchString(match)) {
				continue
			}

			x, y := parseMul(match)
			results = append(results, x*y)
		}
	}

	return results
}

func parseMul(mul string) (int, int) {
	trimmed := strings.NewReplacer("mul(", "", ")", "").Replace(mul)
	parts := strings.Split(trimmed, ",")
	x, _ := strconv.Atoi(parts[0])
	y, _ := strconv.Atoi(parts[1])

	return x, y
}
