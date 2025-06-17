package main

import (
	"fmt"
	"log"
	"os"
	"path/filepath"
	"strconv"
	"strings"
)

func Day1() {
	filePath := filepath.Join("inputs", "day1.txt")
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal(err)
	}
	// Split the data into lines and clean up any whitespace and \r and \n characters
	cleanLines := strings.Split(strings.TrimSpace(string(data)), "\n")
	for i, line := range cleanLines {
		cleanLines[i] = strings.TrimSpace(line)
	}
	// Split the lines into two lists based on the format "number   number"

	listOne, listTwo := SplitLists(cleanLines)
	fmt.Println("List One:", listOne)
	fmt.Println()
	fmt.Println("List Two:", listTwo)
}

func SplitLists(lines []string) ([]int, []int) {
	listOne := []int{}
	listTwo := []int{}

	for _, line := range lines {
		numbers := strings.Split(line, "   ")
		numOne, err := strconv.Atoi(numbers[0])
		if err != nil {
			log.Fatal(err)
		}
		numTwo, err := strconv.Atoi(numbers[1])
		if err != nil {
			log.Fatal(err)
		}
		listOne = append(listOne, numOne)
		listTwo = append(listTwo, numTwo)
	}

	return listOne, listTwo
}
