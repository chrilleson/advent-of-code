package utils

import (
	"log"
	"os"
	"path/filepath"
)

func ReadInputFile(year string, fileName string) []byte {
	// Running from src/go-lang/: ../../.inputs/2024/day1.txt
	filePath := filepath.Join("..", "..", ".inputs", year, fileName+".txt")
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal(err)
	}
	return data
}
