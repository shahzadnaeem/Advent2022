
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
