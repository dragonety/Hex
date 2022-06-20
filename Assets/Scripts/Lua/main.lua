local main = {}

function main.start()
    print('Start')
    ---@type UnityEngine.GameObject
    local go = CS.UnityEngine.GameObject.Find('Cube')
    CS.UnityEngine.Debug.LogError(go.name)
    CS.UnityEngine.GameObject.Find("")
end

return main