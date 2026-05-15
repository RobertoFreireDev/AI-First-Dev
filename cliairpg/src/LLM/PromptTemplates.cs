namespace CLIAirpg.LLM;

public class PromptTemplates
{
    public static string LocationDescription(string locationName, string baseDescription)
    {
        return $"""
You are a fantasy RPG narrator. Describe the location '{locationName}' in 2-3 sentences, 
making it atmospheric and engaging. Be concise.

Base description: {baseDescription}
""";
    }

    public static string ItemDescription(string itemName, string baseDescription)
    {
        return $"""
You are a fantasy RPG narrator. Describe the item '{itemName}' in 1-2 sentences, 
adding flavor and lore. Be concise.

Base description: {baseDescription}
""";
    }

    public static string CombatNarration(string playerName, string enemyName, string action)
    {
        return $"""
Describe a brief, dramatic moment in a combat encounter:
- Player: {playerName}
- Enemy: {enemyName}
- Action: {action}

Keep it to 1-2 sentences, vivid and exciting.
""";
    }

    public static string NPCDialogue(string npcName, string npcPersonality, string topic)
    {
        return $"""
You are {npcName}, a {npcPersonality} NPC in a fantasy RPG.
The player wants to talk about: {topic}

Respond in character with a short, engaging response (1-2 sentences).
""";
    }
}
