# Project Acquiesce

> *A progressive lore companion for Final Fantasy XIV, layered directly onto the MSQ experience.*

Final Fantasy XIV's story is one of the greatest in gaming. But A Realm Reborn throws dozens of proper nouns, factions, nations, and historical events at players before any emotional attachment has formed. Many new players quietly disengage.

**Project Acquiesce** is a [Dalamud](https://dalamud.dev/) plugin that gently surfaces contextual lore information as you encounter it — quest by quest, term by term, without spoilers, without interrupting immersion.

---

## How It Works

When you're on an MSQ quest, key lore terms are quietly available in the **Acquiescence Archive** — an in-game glossary window styled as a Sharlayan archival document.

- Terms are curated **per quest** — you only see what's relevant right now
- Entries expand as you progress through the MSQ (no spoilers for content you haven't reached)
- Toggle between a **Simple** summary and **Expanded** lore context
- The glossary tracks every term you've encountered across your playthrough

This is not a text scanner. It's a **curated contextual layer** built by the community, quest by quest.

---

## Current Coverage

| Expansion | Quests Covered | Terms Indexed |
|-----------|---------------|---------------|
| A Realm Reborn | 2 / ~240-243 | 14 |
| Heavensward | — | — |
| Stormblood | — | — |
| Shadowbringers | — | — |
| Endwalker | — | — |
| Dawntrail | — | — |

**This project is in active early development.** See [Contributing](#contributing).

---

## Installing (for players)

1. Install [XIVLauncher](https://goatcorp.github.io/)
2. Enable the Dalamud plugin system
3. Add our custom plugin repository *(coming soon — pending initial release)*
4. Search for **Project Acquiesce** in the plugin installer

---

## Building Locally (for developers)

**Requirements:**
- .NET 8 SDK
- A working XIVLauncher + Dalamud installation
- `DALAMUD_HOME` environment variable pointing to your Dalamud runtime folder

```bash
git clone https://github.com/yourusername/ProjectAcquiesce
cd ProjectAcquiesce
dotnet build
```

Copy the build output to your Dalamud dev plugin folder, or use the Dalamud dev plugin path feature.

For a full setup guide: https://dalamud.dev/plugin-development/getting-started

---

## Contributing

**You don't need to know how to code to contribute.** The most important work is the data.

### Adding Quest Coverage (No Coding Required)

The plugin reads from simple JSON files in `/data/arr/quests/`. Each file covers one MSQ quest.

**To add a new quest:**

1. Find the quest on [FFXIV Console Games Wiki](https://ffxiv.consolegameswiki.com/wiki/Main_Scenario_Quests)
2. Note the quest ID from the URL or infobox
3. Copy an existing quest file as a template
4. Add only the terms worth highlighting (see guidelines below)
5. Add any new terms to `terms.json` with Simple and Expanded descriptions
6. Open a Pull Request

### What qualifies as a highlight term?

✅ **Highlight these:**
- Nations and city-states
- Named political or military factions
- Beast tribes
- Named deities or primals
- Major historical events
- Recurring institutions (guilds, orders, etc.)

❌ **Do not highlight:**
- Generic words (adventurer, realm, guild)
- One-off NPC names (unless they recur significantly)
- Emotional dialogue terms
- Anything that would constitute a spoiler for the current quest

### Adding Term Descriptions

Term descriptions in `terms.json` should be:
- **Simple**: 1–2 sentences. What does a new player need to know *right now*?
- **Expanded**: 3–5 sentences. Deeper context, still spoiler-free for that quest's expansion

### Other Ways to Help

- **Plugin developers (C#):** See open issues tagged `dev`
- **UI designers:** The glossary window needs a proper Sharlayan aesthetic pass — see issues tagged `ui`
- **Lore reviewers:** Check accuracy of term descriptions — see issues tagged `lore`
- **Moderators:** Join our [Discord](#) to help coordinate contributors

---

## Project Structure

```
ProjectAcquiesce/
├── src/
│   ├── Plugin.cs              # Entry point
│   ├── Services/
│   │   ├── LoreManager.cs     # Loads and serves JSON data
│   │   └── QuestService.cs    # Detects active MSQ quest
│   ├── Windows/
│   │   └── GlossaryWindow.cs  # The Archive UI
│   └── Models/
│       └── LoreModels.cs      # Data structures
├── data/
│   ├── terms.json             # Global lore term database
│   └── arr/
│       └── quests/            # One JSON file per MSQ quest
└── ProjectAcquiesce.csproj
```

---

## Design Philosophy

- **Immersion first.** The plugin should feel like it belongs in the world.
- **Clarity without spoilers.** Entries only reveal what's appropriate for your current MSQ position.
- **Curated, not automated.** Every term is a deliberate human decision.
- **Open and community-owned.** This project should outlast any single contributor.

The aesthetic target is a Sharlayan scholarly archive — measured, authoritative, understated.

---

## Licence

MIT — do whatever you want with it, but please contribute improvements back.

---

*"Compiled for the benefit of wandering scholars and wayward adventurers alike."*
