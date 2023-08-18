using Microsoft.EntityFrameworkCore;
using TagLikeMath.Common;
using TagLikeMath.Data;
using TagLikeMath.Services;

string rules = File.ReadAllText("D:\\MRIYA\\MRIYA\\MRIYA_git\\Microservices\\TagLikeMathRules.json");

_ = TagLikeMathRules.getInstance(rules);
_ = ConnectionRules.getInstance(rules);

BackService back = new BackService();
back.Start();