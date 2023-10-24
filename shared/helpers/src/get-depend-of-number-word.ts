/**
 * @example
 * getDependOfNumberWord(1, ['День', 'Дня', 'Дней'])) // return 'День'
 * getDependOfNumberWord(3, ['День', 'Дня', 'Дней'])) // return 'Дня'
 * getDependOfNumberWord(5, ['День', 'Дня', 'Дней'])) // return 'Дней'
 */

export const getDependOfNumberWord = (number: number, words: string[]) => {
  const cases = [2, 0, 1, 1, 1, 2]

  return words[
    number % 100 > 4 && number % 100 < 20 ? 2 : cases[number % 10 < 5 ? Math.abs(number) % 10 : 5]
  ]
}
