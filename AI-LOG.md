# AI-LOG

Write down, in order, what you asked the AI. It is not a punishment: a missing or false log lowers the level D grade.

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| 2m 14s | I ran a custom skill inside claude code callled "/exam-start", the reason I prepped these skill was to run an inittial commit and analyze the codebase, and run an initial build, call all enpoints. | Well nothing changed in the code, so all that happened is I got a quick rundown of what exactly the code does, and where everything is located. | There are no changes this session .|
| 8m 53s| After the initial run of the prompt it prompted me with a plan which I then told to run 3 subagents to start working on the 5 slices.| Most of what it suggested including removing exception middleware use and building the handlers properly.| Nothing yet.|
| 1m 17s| I ran a skill called "/exam-feature" which analyzez the new feature requirement the professor gave me and  modified the plan given the new requirements| Given the new feature it made the changes requested by the professor.| Only modified the naming of specific components to make it more clear and readable for me.|
| 24s| I ran a skill called "/exam-bugs" to check the list of bugs and see all the starter issues to see what I need to continue working on.| Nothing.| Nothing.|
| 5m 11s| I started the bug fixes by using another skill i made called "/exam-bugs-fix" and named the exact bug in the list, i did this for each on so I just wrote the total time for the run for all the initial starter bugs. | I kept most of the code and made slight changes to adhere to what the broken features should do. Currently only 5 are fixed.| I changed the order in whcih the AI model wanted to approach beacue i found it easier to run 1 agent for each of these bugs and let them communicate with each other so they don't break the codebase.|
| 5m 11s| Ran bug fix for "/exam-bugs-fix 6"  and "/exam-bugs-fix 7" at a later time than the other to first check that everything was working before i continued.| Same reasoning as above.| same reasoning as above|
