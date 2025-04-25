using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CourseDisplacementRocketView : View
{
    [SerializeField] private TextMeshProUGUI textCourse;

    public void SetCourse(int course)
    {
        textCourse.text = $"{course}m";
    }
}
