// NPC Dialogue - DreamWalker
// Placeholder dialogue for NPCs

-> start

=== start ===
[NPC speaks to you]

This is placeholder dialogue text. The actual lines will be added later.

* [Option 1: Ask about the area]
    -> option_1
* [Option 2: Ask for help]
    -> option_2
* [Option 3: Say goodbye]
    -> end_conversation

=== option_1 ===
This area holds many secrets, traveler.

Placeholder text: More information about the area will go here.

* [Continue asking questions]
    -> start
* [Say goodbye]
    -> end_conversation

=== option_2 ===
I can help you, but first you must help yourself.

Placeholder text: Quest or hint information will go here.

* [Ask something else]
    -> start
* [Leave]
    -> end_conversation

=== end_conversation ===
Farewell, wanderer. Good luck on your journey.
-> END
