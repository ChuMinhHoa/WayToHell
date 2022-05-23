using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSkill100Crits : SkillBase
{
    public Transform shotPoint;
    public GameObject guildeLine;
    public LineRenderer line;
    float skillAngle;
    public GameObject bullet;
    public override void UpdateSkill()
    {
        switch (skillState)
        {
            case SkillState.Ready:
                if (Input.GetKey(key))
                {
                    skillState = SkillState.Hold;
                    guildeLine.SetActive(true);
                    weaponBase.isUsingSkill = true;
                }
                break;
            case SkillState.Hold:
                float viewDefaut = CameraControllerCustom.instance.viewfarDefault;
                CameraControllerCustom.instance.viewfarPoint = viewDefaut * 2;
                CameraControllerCustom.instance.cameraState = CameraState.increaseViewFar;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distance = Vector2.Distance(mousePos, transform.position);
                if (distance < 2)
                    distance = 1;
                else distance = 2 / distance;
                Vector2 dr = (mousePos - (Vector2)shotPoint.position);
                dr *= distance;
                line.SetPosition(0, (Vector2)shotPoint.position);
                line.SetPosition(1, (Vector2)shotPoint.position + dr);
                if (Input.GetKeyUp(key))
                {
                    skillState = SkillState.Activate;
                    Vector2 direction = (line.GetPosition(1) - line.GetPosition(0)).normalized;
                    skillAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    guildeLine.SetActive(false);
                    weaponBase.isUsingSkill = false;
                }
                break;
            case SkillState.Activate:
                CameraControllerCustom.instance.cameraState = CameraState.resetView;
                Activate(skillAngle);
                activateTime = activateTimeSetting;
                skillState = SkillState.UsingSkill;
                break;
            case SkillState.UsingSkill:
                if (activateTime > 0)
                    activateTime -= Time.deltaTime;
                else
                {
                    coldDown = coldDownSetting;
                    skillState = SkillState.ColdDown;
                }
                break;
            case SkillState.ColdDown:
                if (coldDown > 0)
                    coldDown -= Time.deltaTime;
                else
                {
                    skillState = SkillState.Ready;
                }
                break;
            default:
                break;
        }
    }
    public override void Activate(float angle)
    {
        base.Activate(angle);
        GameObject bulletCreate = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        bulletCreate.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
