
# Just messing around with the quite cryptic BQN
#
# Managed to add comments and make a small change to the example in the video below!
#
# YouTube: Advent of Code 2022 in APL & BQN Day 6!
# Link:    https://www.youtube.com/watch?v=A-sUyER70gk

data ← "mjqjpqmgbljsphdztnvjfqwrcgsmlb"

SplitAt ← { 𝕨 (↑⋈↓) 𝕩 }
FlipSplitAt ← { 𝕨 (↑˜⋈↓˜) 𝕩 }

             #Pair with parameters
                       #Pair of prefix and remainder based on result 
                                 #First (Unbox)
                                   #Add 𝕨 for position
                                     #index of 𝕨
                                         #length
                                            #dedup
                                               #to list
                                                 # window
Solution2 ← {(𝕨 ⋈ 𝕩) ⋈ 𝕩 (↑˜⋈↓˜) ⊑ 𝕨+𝕨⊐˜ ≠∘⍷¨ <˘ 𝕨↕𝕩}

Solution0 ← {⊑ 𝕨+𝕨⊐˜ ≠∘⍷¨ <˘ 𝕨↕𝕩}
# Optimised
Solution ← {⊑ 𝕨+𝕨⊐˜ ≠∘⍷˘ 𝕨↕𝕩}

# •Show allows intermediate values to be displayed, otherwise only last one is

•Show 14 SplitAt data
•Show 3 SplitAt¨ 10 SplitAt data

•Show 4 Solution2 data
14 Solution2 data

## More stuff ...

data ← "mjqjpqmgbljsphdztnvjfqwrcgsmlb"

Solution ← {(⊢⋈⍷)¨<˘𝕨↕𝕩}

•Show 14 Solution data

•Show "Simple expressions"
•Show ¯1.5 + π
•Show ÷ ∞
•Show 'e' ⋈ ⋆ 1
•Show √ 2
•Show 3 √ 8

# Right to left evaluation!!!
•Show "Eval is Right to Left!"
•Show √ 3 + 2 × √2
•Show "Not" ⋈ (√3)+2×√2
•Show 3+4×5-7
•Show "Not" ⋈ 3+(4×5)-7

# Undo (Inverse)
•Show "Undo (Inverse)"
•Show √⁼ 25
•Show 3 √⁼ 25
•Show 25 × 25 × 25

# 1-Modifiers
Const4 ← 4˙
SwappedDivide ← ÷˜

•Show "More 1-Modifiers"
•Show 4 SwappedDivide 2
•Show 4 ÷ 2
•Show 1 Const4 1.4

# 2-Modifiers
AddThenSquare ← ×˜∘+   # Square by self multiply
AddThenSquare2 ← √⁼∘+  # Square by Undo √

•Show "2-Modifiers"
•Show 3 AddThenSquare 4
•Show 4 AddThenSquare 3
•Show AddThenSquare 4
•Show 3 AddThenSquare2 4

# Tables and stuff - just a start

# Times table
•Show ×⌜˜ 1+↕6

# Zip - must be same shape
•Show "ABC" ⋈¨ "123"

# Table (Cartesian Product)
•Show 1‿2‿3 ⋈⌜ 20‿40

# To List
•Show <˘ 1‿2 ⋈⌜ 20‿40

# First of First of First of List from Table
•Show ⊑ ⊑ ⊑ <˘ 1‿2 ⋈⌜ 20‿40

"Done"
