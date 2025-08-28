import { describe, expect, test } from "bun:test";
import { readFileSync } from "fs";
import { dirname, join } from "path";
import { fileURLToPath } from "url";

const __dirname = dirname(fileURLToPath(import.meta.url));

describe("Basic Parameterization", () => {
    const stringValues = ["foo", "bar", "###", "___", "   "];

    stringValues.forEach((value) => {
        test(`test with string value: "${value}"`, () => {
            expect(typeof value).toBe("string");
            expect(value.length).toBeGreaterThan(0);
        });
    });

    const stringsWithEmptyAndNull = ["foo", "bar", "", null];

    stringsWithEmptyAndNull.forEach((value) => {
        test(`test with strings including empty and null: ${value}`, () => {
            if (value !== null) {
                expect(typeof value).toBe("string");
            }
        });
    });

    const numbers = [1, 2, 3, 7, 8, 9, 0, -1, -2, -2147483648];

    numbers.forEach((number) => {
        test(`test with integer: ${number}`, () => {
            expect(typeof number).toBe("number");
            expect(Number.isInteger(number)).toBe(true);
        });
    });
});

describe("Multiple Parameters", () => {
    const uppercaseTestCases = [
        { input: "test", expected: "TEST" },
        { input: "tEst", expected: "TEST" },
        { input: "Java", expected: "JAVA" },
        { input: "Python", expected: "PYTHON" },
    ];

    uppercaseTestCases.forEach(({ input, expected }) => {
        test(`string uppercase: ${input} -> ${expected}`, () => {
            expect(input.toUpperCase()).toBe(expected);
        });
    });

    const additionTestCases = [
        { a: 2, b: 3, expected: 5 },
        { a: 0, b: 0, expected: 0 },
        { a: -1, b: 1, expected: 0 },
        { a: 100, b: 200, expected: 300 },
    ];

    additionTestCases.forEach(({ a, b, expected }) => {
        test(`addition: ${a} + ${b} = ${expected}`, () => {
            expect(a + b).toBe(expected);
        });
    });
});

describe("Parameterize with IDs", () => {
    const stringLengthCases = [
        { value: "hello", expected: 5, id: "normal_string" },
        { value: "", expected: 0, id: "empty_string" },
        { value: "pytest", expected: 6, id: "framework_name" },
    ];

    stringLengthCases.forEach(({ value, expected, id }) => {
        test(`string length [${id}]: "${value}" has length ${expected}`, () => {
            expect(value.length).toBe(expected);
        });
    });
});

describe("Cartesian Product", () => {
    const xValues = [1, 2, 3];
    const yValues = [10, 20];

    xValues.forEach((x) => {
        yValues.forEach((y) => {
            test(`cartesian product: ${x} < ${y}`, () => {
                expect(x).toBeLessThan(y);
            });
        });
    });
});

describe("Test with Marks", () => {
    const evalExpressions = [
        { input: "3+5", expected: 8, id: "addition" },
        { input: "2*4", expected: 8, id: "multiplication" },
        { input: "6-2", expected: 4, id: "subtraction" },
        { input: "1/0", expected: 0, shouldThrow: true },
    ];

    evalExpressions.forEach(({ input, expected, id, shouldThrow }) => {
        if (shouldThrow) {
            test.skip(`eval expression [${id || "skip"}]: ${input} = ${expected}`, () => {
                // This would throw, so we skip it
                expect(eval(input)).toBe(expected);
            });
        } else {
            test(`eval expression [${id}]: ${input} = ${expected}`, () => {
                expect(eval(input)).toBe(expected);
            });
        }
    });

    const squareRootCases = [
        { value: -1, skip: true, reason: "negative numbers not supported" },
        { value: 0 },
        { value: 1 },
        { value: 4 },
        { value: 9 },
    ];

    squareRootCases.forEach(({ value, skip, reason }) => {
        if (skip) {
            test.skip(`square root of ${value} - ${reason}`, () => {
                const result = Math.sqrt(value);
                expect(result * result).toBe(value);
            });
        } else {
            test(`square root of ${value}`, () => {
                const result = Math.sqrt(value);
                expect(result * result).toBe(value);
            });
        }
    });
});

function generatePrimeNumbers(maxValue = 100): number[] {
    function isPrime(n: number): boolean {
        if (n < 2) return false;
        for (let i = 2; i <= Math.sqrt(n); i++) {
            if (n % i === 0) return false;
        }
        return true;
    }

    const primes: number[] = [];
    for (let n = 2; n < maxValue; n++) {
        if (isPrime(n)) primes.push(n);
    }
    return primes;
}

describe("Parameterize with Function", () => {
    const primes = generatePrimeNumbers(50);

    primes.forEach((prime) => {
        test(`prime number: ${prime}`, () => {
            for (let divisor = 2; divisor < prime; divisor++) {
                expect(prime % divisor).not.toBe(0);
            }
        });
    });
});

describe("Parameterize from File", () => {
    function loadCsvData(): Array<{
        input: string;
        expectedUpper: string;
        expectedLen: number;
    }> {
        const csvPath = join(__dirname, "test-data.csv");
        const content = readFileSync(csvPath, "utf-8");
        const lines = content.trim().split("\n");
        lines.shift(); // Skip header

        return lines.map((line) => {
            const [input, expectedUpper, expectedLen] = line.split(",");
            if (!input || !expectedUpper || !expectedLen) {
                throw new Error(`Invalid CSV line: ${line}`);
            }
            return {
                input,
                expectedUpper,
                expectedLen: Number.parseInt(expectedLen, 10),
            };
        });
    }

    const csvData = loadCsvData();

    csvData.forEach(({ input, expectedUpper, expectedLen }) => {
        test(`from CSV: "${input}" -> upper: "${expectedUpper}", length: ${expectedLen}`, () => {
            expect(input.toUpperCase()).toBe(expectedUpper);
            expect(input.length).toBe(expectedLen);
        });
    });
});

describe("Complex Parameterization", () => {
    interface Config {
        name: string;
        value: number;
        enabled: boolean;
    }

    const configs: Config[] = [
        { name: "test1", value: 10, enabled: true },
        { name: "test2", value: 20, enabled: false },
        { name: "test3", value: 30, enabled: true },
    ];

    configs.forEach((config) => {
        test(`with dict params: ${config.name}`, () => {
            expect(typeof config.name).toBe("string");
            expect(typeof config.value).toBe("number");
            expect(typeof config.enabled).toBe("boolean");

            if (config.enabled) {
                expect(config.value).toBeGreaterThan(0);
            }
        });
    });
});

describe("TypeScript Match Expression", () => {
    const matchTestCases = [
        { input: 1, expected: "one" },
        { input: 2, expected: "two" },
        { input: 3, expected: "three" },
        { input: 99, expected: "other" },
    ];

    matchTestCases.forEach(({ input, expected }) => {
        test(`match expression: ${input} -> "${expected}"`, () => {
            let result: string;
            switch (input) {
                case 1:
                    result = "one";
                    break;
                case 2:
                    result = "two";
                    break;
                case 3:
                    result = "three";
                    break;
                default:
                    result = "other";
            }
            expect(result).toBe(expected);
        });
    });
});

describe("Nested describe blocks for organization", () => {
    describe("Number Operations", () => {
        const values = [1, 2, 3];

        describe("is positive", () => {
            values.forEach((value) => {
                test(`${value} is positive`, () => {
                    expect(value).toBeGreaterThan(0);
                });
            });
        });

        describe("square", () => {
            values.forEach((value) => {
                test(`square of ${value} is positive`, () => {
                    expect(value * value).toBeGreaterThan(0);
                });
            });
        });

        describe("double", () => {
            values.forEach((value) => {
                test(`double of ${value} is greater than ${value}`, () => {
                    expect(value * 2).toBeGreaterThan(value);
                });
            });
        });
    });
});
