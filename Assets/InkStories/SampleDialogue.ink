// Sample Dialogue - DreamWalker
// This is placeholder text that will be replaced with actual dialogue later

-> main

=== main ===
Hello, traveler. Welcome to the dream realm.
I sense you have many questions.
* [Who are you?]
    -> who_are_you
* [Where am I?]
    -> where_am_i
* [I need to go.]
    -> goodbye

=== who_are_you ===
I am a guide in this realm between waking and sleeping.
Some call me the Keeper of Dreams.
* [Tell me more about this place.]
    -> where_am_i
* [I should continue my journey.]
    -> goodbye

=== where_am_i ===
You are walking between worlds, in the space where dreams take form.
This is a place of memories, fears, and hopes given shape.
* [How do I leave?]
    -> how_to_leave
* [Thank you for the explanation.]
    -> goodbye

=== how_to_leave ===
To leave, you must first understand why you came.
The path forward will reveal itself when you are ready.
* [I understand.]
    -> goodbye

=== goodbye ===
Safe travels, dreamer. May you find what you seek.
-> END
