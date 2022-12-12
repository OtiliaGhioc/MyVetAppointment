namespace VetAppointment.Tests
{
    public class UserTests
    {
        [Fact]
        public void TestUserInfo()
        {
            User user = new User("name", "pass", true);
            Assert.AreEqual("name", user.Username);
            //Assert.AreEqual("pass", user.Password);
            Assert.IsFalse(user.HasOffice);
        }

        [Fact]
        public void TestUserRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            UserRepository userRepo = new UserRepository(testDb);
            User user = new User("name", "pass", true);

            TestAdd(userRepo, user);
            TestGet(userRepo, user);
            TestAll(userRepo, user);

            Expression<Func<User, bool>> predicate = u => u.Username.Equals(user.Username);
            TestFind(userRepo, user, predicate);

            TestDelete(userRepo, user);
        }

        private async void TestAdd(UserRepository userRepo, User user)
        {
            User added = await userRepo.Add(user);
            await userRepo.SaveChanges();
            Assert.AreEqual(user, added);
        }

        private async void TestGet(UserRepository userRepo, User user)
        {
            Assert.AreEqual(user, await userRepo.Get(user.UserId));
        }

        private async void TestAll(UserRepository userRepo, User user)
        {
            var allUsers = await userRepo.All();
            bool check = false;

            if (allUsers.Contains<User>(user))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestFind(UserRepository userRepo, User user, Expression<Func<User, bool>> predicate)
        {
            var foundUsers = await userRepo.Find(predicate);
            bool check = false;

            if (foundUsers.Contains<User>(user))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestDelete(UserRepository userRepo, User user)
        {
            await userRepo.Delete(user);
            await userRepo.SaveChanges();
            Assert.IsNull(await userRepo.Get(user.UserId));
        }
    }
}