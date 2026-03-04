# Contributing to Project Acquiesce

Thanks for your interest. This guide covers everything you need to start contributing — regardless of your technical background.

---

## The Most Important Thing First

**You do not need to know how to code.**

The heart of this project is the data — the quest files and term descriptions in `/data/`. Anyone who knows FFXIV's lore and has half an hour can make a meaningful contribution.

---

## Data Contributions (No Coding Required)

### Step 1 — Pick an uncovered quest

Check the [quest coverage table in README.md](README.md#current-coverage) or look in `/data/arr/quests/` to see which quests already have files. Pick an MSQ quest that isn't covered yet.

Use [FFXIV Console Games Wiki — Main Scenario Quests](https://ffxiv.consolegameswiki.com/wiki/Main_Scenario_Quests) as your reference.

### Step 2 — Create the quest file

Name your file: `QUESTID_quest_name_with_underscores.json`

Quest IDs can be found in the wiki URL or infobox.

```json
{
  "QuestId": 65603,
  "QuestName": "A Bad Bladder",
  "Expansion": "ARR",
  "HighlightTerms": [
    "Twelveswood",
    "Wood Wailers"
  ]
}
```

Only include terms that are **worth explaining** to a new player right now. When in doubt, leave it out.

### Step 3 — Add any new terms to terms.json

If your quest introduces a term that doesn't exist in `terms.json` yet, add it:

```json
"New Term Here": {
  "Category": "Nation",
  "IntroducedIn": 65603,
  "Simple": "A one or two sentence explanation a new player needs right now.",
  "Expanded": "Deeper context — still spoiler-free for this expansion."
}
```

**Categories:** Nation · Location · Faction · Institution · Beast Tribe · Historical Event · Mythic Figures · Entities · Political Figure

### Step 4 — Open a Pull Request

Even if your JSON isn't perfect, open a PR. The lore review team will check it before merge.

---

## What Makes a Good Highlight Term?

Ask yourself: *If a new player saw this word and had no context, would not knowing it make the story harder to follow?*

If yes → highlight it.  
If no → skip it.

### Good candidates
- Nations, city-states, regions
- Named military or political factions
- Beast tribes
- Named primals or deities
- Major historical events (Calamities, wars, etc.)
- Recurring guilds or institutions

### Not worth highlighting
- Generic nouns (adventurer, guild, realm, forest)
- Dialogue-only NPC names that don't recur
- Emotional/descriptive language
- Anything that would spoil future quests

---

## Code Contributions

If you're a C# developer familiar with Dalamud, the most impactful open problems are:

1. **Dialogue hook** — detecting when specific quest dialogue is on screen and overlaying term highlights directly on the text (see `GlossaryWindow.cs` for current state)
2. **Progression gating** — only showing Expanded term entries after the player has passed a certain MSQ threshold
3. **UI polish** — the Sharlayan archive aesthetic is the design target; the current ImGui implementation is functional but not beautiful

Check GitHub Issues for tagged `dev` and `ui` tasks.

---

## Lore Review

If you're confident in your FFXIV lore knowledge, you can help review term descriptions submitted by other contributors. Look for PRs tagged `needs-lore-review`.

Criteria:
- Accurate for the MSQ stage it's introduced at
- Spoiler-free for content beyond that expansion
- Written clearly for someone who has never played FFXIV before

---

## Style Guide for Term Descriptions

**Simple (aim for 1–2 sentences):**
- Present tense
- No jargon beyond what's been established in-game by this point
- Answers: "What is this, and why does it matter right now?"

**Expanded (aim for 3–5 sentences):**
- Can include more world-building context
- Still constrained to no spoilers beyond the term's introduction expansion
- Answers: "What deeper context helps me understand this?"

**Do not:**
- Reference future plot events
- Use Wikipedia-style encyclopaedia tone
- Copy any text verbatim from third-party sources

---

*Questions? Join the [Discord](#) or open a GitHub Discussion.*
