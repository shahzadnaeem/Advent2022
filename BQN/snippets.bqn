
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

Solution ← {⊑ 𝕨+𝕨⊐˜ ≠∘⍷¨ <˘ 𝕨↕𝕩}

•Show 4 Solution data
      14 Solution data
