using JsonDb;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace JsonDbTests
{
    public class UnitTest1
    {
        [Fact]
        public void EnsureCanReadFileFromLocation()
        {
            string text = File.ReadAllText(Path.Combine(GetTestFileFolder(), "test.json"));
            Assert.Equal("{ \"name\": \"roger\" }", text);
        }

        [Fact]
        public void CalculateTaskProgressComplete()
        {
            var task = new Task();
            task.Id = "A9D3DD63-A0F3-4E02-A14A-8DA64CF923C3";
            task.Title = "New Task";
            task.Initial = 50;
            task.Target = 150;
            task.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-13 12:13:20"), Value = 75 });
            task.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-13 12:13:20"), Value = 100 });
            task.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-13 12:13:20"), Value = 125 });

            var percentComplete = task.PercentCompleted();
            Assert.Equal(75, percentComplete);
        }

        [Fact]
        public void CalculateGoalProgressComplete()
        {
            var goal = new GoalDocument();
            goal.Id = "A9D3DD63-A0F3-4E02-A14A-8DA64CF923C3";
            goal.Title = "Test Goal";

            var task1 = new Task() { Id = "09b56e3d-49f0-44b6-b063-1e362f8282ce", Title = "Task 1", Initial = 0, Target = 100 };
            var task2 = new Task() { Id = "9e4b27e6-3c9d-448b-8a77-170eb59438c6", Title = "Task 2", Initial = 0, Target = 100 };

            task1.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-13"), Value = 25 });
            task1.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-14"), Value = 50 });

            task2.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-13"), Value = 25 });
            task2.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-14"), Value = 100 });

            goal.Tasks.Add(task1);
            goal.Tasks.Add(task2);

            var percentComplete = goal.GetPercentComplete();
            Assert.Equal(75, percentComplete);
        }

        [Fact]
        public void GetGoalList()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var goalList = repository.GetGoalList($"goals.json");

            Assert.Equal(6, goalList.Count);
        }

        [Fact]
        public void SaveGoalList()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var goalList = repository.GetGoalList($"goals.json");
            goalList.Add(new GoalListItem { Id = "8D642D0F-9CE1-4CF9-8CA6-828DFA25214E", PercentComplete = 99, Title = "Added Goal" });

            repository.SaveGoalList(goalList, $"saved_goals.json");

            var goalListNew = repository.GetGoalList($"saved_goals.json");

        }

        [Fact]
        public void GetGoalDocument()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var goal = repository.GetGoalDocument("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E");

            Assert.Equal("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E", goal.Id);
            Assert.Equal("Highway to Hell Solo", goal.Title);
        }

        [Fact]
        public void SaveGoalDocument()
        {
            //var repository = new GoalRepository(GetTestFileFolder());
            //var goal = repository.GetGoalDocument("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E");

            //Assert.Equal("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E", goal.Id);
            //Assert.Equal("Highway to Hell Solo", goal.Title);

            var document = new GoalDocument();
            document.Id = "A9D3DD63-A0F3-4E02-A14A-8DA64CF923C3";
            document.Title = "New Goal Document";


            // document.Tasks.Add()
        }

        [Fact]
        public void GetTask()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var task = repository.GetTask("290D51D0-6918-41CE-9734-8EA7870DB218");
            var activity = task.Activity[0];

            Assert.Equal("290D51D0-6918-41CE-9734-8EA7870DB218", task.Id);
            Assert.Equal("Highway to Hell - Lick 2", task.Title);
            Assert.Equal(0, task.Initial);
            Assert.Equal(100, task.Target);
            Assert.Equal(3, task.Activity.Count);
            Assert.Equal(DateTime.Parse("2019-05-04"), activity.Date);
        }

        [Fact]
        public void SaveTask()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var task = new Task();
            task.Id = "A9D3DD63-A0F3-4E02-A14A-8DA64CF923C3";
            task.Title = "New Task";
            task.Initial = 5;
            task.Target = 200;
            task.Activity.Add(new TaskActivity() { Date = DateTime.Parse("2020-03-13"), Value = 88 });

            repository.SaveTask(task);

            var savedTask = repository.GetTask("A9D3DD63-A0F3-4E02-A14A-8DA64CF923C3");

            Assert.Equal("A9D3DD63-A0F3-4E02-A14A-8DA64CF923C3", savedTask.Id);
            Assert.Equal("New Task", savedTask.Title);
            Assert.Equal(5, savedTask.Initial);
            Assert.Equal(200, savedTask.Target);
            Assert.Equal(1, savedTask.Activity.Count);
            Assert.Equal(DateTime.Parse("2020-03-13"), savedTask.Activity[0].Date);
            Assert.Equal(88, savedTask.Activity[0].Value);
        }

        [Fact]
        public void GetGoal()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var goal = repository.GetGoal("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E");
            var taskReferences = goal.TaskReferences[0];

            Assert.Equal("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E", goal.Id);
            Assert.Equal("Highway to Hell Solo", goal.Title);
            Assert.Equal(2, goal.TaskReferences.Count);
            Assert.Equal("9306A3B1-6AAD-43F6-B083-C4D9FD045048", taskReferences.Id);
        }

        [Fact]
        public void LoadGoalTasksFromTaskReferences()
        {
            var repository = new GoalRepository(GetTestFileFolder());
            var goal = repository.GetGoal("8D642D0F-9CE1-4CF9-8CA6-828DFA25214E");
            var tasks = repository.GetTasks(goal.TaskReferences);

            Assert.Equal(2, tasks.Count);
            Assert.Equal("290D51D0-6918-41CE-9734-8EA7870DB218", tasks[1].Id);
            Assert.Equal("Highway to Hell - Lick 2", tasks[1].Title);
            Assert.Equal(0, tasks[1].Initial);
            Assert.Equal(100, tasks[1].Target);
            Assert.Equal(3, tasks[1].Activity.Count);
            Assert.Equal(DateTime.Parse("2019-05-04"), tasks[1].Activity[0].Date);
            Assert.Equal(33.33, tasks[1].Activity[0].Value);
        }

        [Fact]
        public void LoadTasks()
        {

        }

        private string GetTestFileFolder()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData");
        }

    }
}
