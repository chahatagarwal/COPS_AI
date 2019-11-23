﻿using System;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Selection;
using Assets.Scripts.Interfaces;

class EvolutionManager : MonoBehaviour
{
    #region properties
    [Header("Car prefabs")]
    public GameObject Police;
    public GameObject Target;

    [Header("Genetic algorithm")]
    public SelectionName SelectionName;
    [Range(1,50)]public int Population = 1;
    public GameObject InitPoints;

    private Quaternion _Rotation = Quaternion.Euler(0, 0, 0);
    private ISelectable _Selection;
    #endregion

    private void Awake()
    {
        _Selection = SelectionFactory.Selection(SelectionName);
        _Selection.Prefab = this.Police;
        _Selection.Positions = GetChildrenPositions();
        _Selection.Rotation = this._Rotation;
        _Selection.Population = this.Population;
        _Selection.Target = this.Target;
        _Selection.CreateFirstGeneration();
        _Selection.PopulationReduced += OnPopulationReduced;
    }

    public void NextGenButton()
    {
        _Selection.CreateNewGeneration();
    }

    private List<Vector2> GetChildrenPositions()
    {
        List<Vector2> positions = new List<Vector2>();
        
        foreach(Transform child in InitPoints.transform)
        {
            positions.Add(child.transform.position);
        }

        return positions;
    }

    private void OnPopulationReduced(object source, PopulationEventArgs e)
    {
        if (e.ActualPopulation==0)
            _Selection.CreateNewGeneration();
    }

    


}
