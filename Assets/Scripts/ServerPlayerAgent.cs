using Unity.Netcode;
using UnityEngine;

/* For multi-agent game, having a "real" player control is a bit silly.
 * It would require to *be* several GameObjects at the same time.
 * Instead, what we do is we have ServerPlayerAgent as a class, instances of which represent the agency of players to telegraph the inputs to the server.
 * The server then authorizes those and moves some associated GameObjects around. */
public class ServerPlayerAgent : NetworkBehaviour {
    [SerializeField]
    private int fuel = 10;
    private bool greet = true;

    [ServerRpc]
    public void StepServerRpc(int amount)
    {
        var owner = OwnerClientId;
        if (amount > fuel)
        {
            Logger.Singleton.LogError($"{owner} is trying to use more fuel than we have: {amount} > {fuel}.");
            amount = fuel;
        }
        fuel -= amount;
        Logger.Singleton.LogWarning($"{owner} has {fuel} fuel cells remaining.");
    }

    private void Update()
    {
        if (greet)
        {
            greet = false;
            Logger.Singleton.LogInfo("Hello from ServerPlayerAgent!");
            Logger.Singleton.LogInfo("The fact that you see this means that this class is used as player prefab.");
            Logger.Singleton.LogInfo("It's half the job done.");
        }
    }

}