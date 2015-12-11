using System;
using System.Collections.Generic;
using ScriptLib.Extensions;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts
{
    public class ScriptCollection : List<Script>
    {
        public void Refractor()
        {
            Dictionary<string, uint> labels = new Dictionary<string, uint>();
            Dictionary<Type, int> counter = new Dictionary<Type, int>();
            foreach (Script s in this)
            {
                for (int i = s.SubSections.Count - 1; i >= 0; --i)
                {
                    uint offset;
                    ISection<ICommand> section = s.SubSections[i];
                    if (section.LabelName.TryParse(out offset))
                    {
                        Type t = section.GetType();
                        if (!labels.ContainsValue(offset))
                        {
                            if (counter.ContainsKey(t))
                            {
                                counter[t]++;
                            }
                            else
                            {
                                counter.Add(t, 0);
                            }
                        }
                        section.LabelName = section.Provider.Name + "_" + counter[t];
                        if (labels.ContainsValue(offset))
                            s.SubSections.Remove(section);
                        else
                            labels.Add(section.LabelName, offset);
                    }
                }

            }
        }
    }
}
