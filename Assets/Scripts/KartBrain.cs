using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;


public class KartBrain : Agent
{
    public CheckpointManager _checkpointManager;
    private KartControllerAI _kartController;

    public override void Initialize()
    {
        _kartController = GetComponent<KartControllerAI>();
    }

    public override void OnEpisodeBegin()
    {
        _checkpointManager.ResetCheckpoints();
        _checkpointManager.setLaps(6);
        _kartController.Respawn();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff / 20f);
        AddReward(-0.001f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;

        switch (actions.DiscreteActions[0])
        {
            case 0: forwardAmount = 0f; break;

            case 1: forwardAmount = +1f; break;

            case 2: forwardAmount = -1f; break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0: turnAmount = 0f; break;

            case 1: turnAmount = +1f; break;

            case 2: turnAmount = -1f; break;
        }

        _kartController.ApplyAcceleration(forwardAmount);
        _kartController.Steer(turnAmount);
        _kartController.AnimateKart(turnAmount);
        //AddReward(-0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.W)) forwardAction = 1;
        if (Input.GetKey(KeyCode.S)) forwardAction = 2;
        int turnAction = 0;
        if (Input.GetKey(KeyCode.D)) turnAction = 1;
        if (Input.GetKey(KeyCode.A)) turnAction = 2;
        var discreateActions = actionsOut.DiscreteActions;
        discreateActions[0] = forwardAction;
        discreateActions[1] = turnAction;
    }
}
