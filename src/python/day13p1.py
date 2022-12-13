# From https://github.com/hyper-neutrino/advent-of-code/blob/main/2022/day13p1.py

import sys


def getInput(file):
    input = open(file).read().strip()
    pairwise_input = input.split("\n\n")
    pairs = map(str.splitlines, pairwise_input)
    return list(map(lambda pair: (eval(pair[0]), eval(pair[1])), pairs))


def compare(l, r):
    if type(l) == int and type(r) == int:
        return l - r
    elif type(l) == int and type(r) == list:
        return compare([l], r)
    elif type(l) == list and type(r) == int:
        return compare(l, [r])

    # Try elements first
    for l2, r2 in zip(l, r):
        res = compare(l2, r2)
        if res:
            return res

    # Finally use list length to decide
    return len(l) - len(r)


if __name__ == "__main__":
    if len(sys.argv) == 1:
        print("Nothing given\n\n")
        exit(1)
    else:
        file = sys.argv[1]
        print(f"Processing {sys.argv[1]} ...")

        input_pairs_list = getInput(file)

        score = 0
        for ix, (l, r) in enumerate(input_pairs_list):
            if compare(l, r) < 0:
                score += (ix + 1)

        print(score)

        exit(0)
