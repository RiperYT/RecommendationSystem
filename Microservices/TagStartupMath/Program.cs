using TagStartupMath.Common;
using TagStartupMath.Services;

string rules = File.ReadAllText("D:\\MRIYA\\MRIYA\\MRIYA_git\\Microservices\\TagLikeMathRules.json");

_ = TagStartupMathRules.getInstance(rules);
_ = ConnectionRules.getInstance(rules);

BackService back = new BackService();
back.Start();