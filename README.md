# Packer

A .Net core 3.1 library tproject hat provides endpoint for calculating the cost effective combination of items for a package using dynamic proogramming approach to 0/1 knapsack problem algo

## Installation
Dowloand the code as zip
Open the solution in Visual Studio by double clicking the solution file in the folder

## Usage
Test console application is provided with the solution to test the library
Set the console application as startup project
In its main method provide absolute path to the file containing test data
Generated result output to console

## Sample Input 
API endpoint accepts absolute path to a file which contains mulitple entries.Each line represents a package containing items
Each item consist of its index, weight and price
Value at start of line represnts package weight limit
Following is the sample input of file which the library Endpoint accepts

8 : (1,6.3,€34)

75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)

##Sample Output
For the above sample input following will be returned by the endpoint

1

2,7

## Credits

TODO: Write credits

## License

TODO: Write license
