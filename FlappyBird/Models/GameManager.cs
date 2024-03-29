﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Models
{
    //public class GameManager : INotifyPropertyChanged
    public class GameManager
    {
        private readonly int _gravity = 2;

        public event PropertyChangedEventHandler MainLoopCompleted;

        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; }
        public bool IsRunning { get; private set; } = false;
        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }
        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();
                MainLoopCompleted?.Invoke(this, null);
                await Task.Delay(20);
            }
        }

        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
                Pipes.Add(new PipeModel());

            if (Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
        }
        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }
        public void Left()
        {
            if (IsRunning)
            {
                Bird.Left();
            }
        }
        public void Right()
        {
            if (IsRunning)
            {
                Bird.Right();
            }
        }
        void MoveObjects()
        {
            Bird.Fall(_gravity);
            //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Bird)));
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
            //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Pipe)));
        }
        void CheckForCollisions()
        {
            if (Bird.IsOnGround())
            {
                GameOver();
            }
            var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());

            if (centeredPipe != null)
            {
                bool hasCollidedWithBottom =Bird.DistanceFromGround < centeredPipe.GapBottom - 150;
                bool hasCollidedWithTop = Bird.DistanceFromGround + 45 > centeredPipe.GapTop - 150;
                bool hasCollidedWithLeft = Bird.DistanceFromLeft >= centeredPipe.DistanceFromLeft;
                if (hasCollidedWithBottom || hasCollidedWithTop)
                    GameOver();
            }
        }
        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
