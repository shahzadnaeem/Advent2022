# From https://github.com/hyper-neutrino/advent-of-code/blob/main/2022/day13p2.py

# Nice solution to find the correct positions

import sys


def getInput(file):
    input = open(file).read().strip()
    lines = input.split()
    return list(map(eval, lines))


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

        input_list = getInput(file)

        # Dividers - whose positions need to be found
        d1 = [[2]]
        d2 = [[6]]

        # Initial positions
        p1 = 1
        p2 = 2

        # Find correct positions
        for l in input_list:
            if compare(l, d1) < 0:
                # Nudge both
                p1 += 1
                p2 += 1
            elif compare(l, d2) < 0:
                # Nudge divider 2
                p2 += 1

        print(p1*p2)

        exit(0)
