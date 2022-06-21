local main = {}

function main.start()
    print('Start')
    ---@type UnityEngine.GameObject
    local go = CS.UnityEngine.GameObject.Find('Cube')
    ---@type UnityEngine.Renderer
    local render = go:GetComponent(typeof(CS.UnityEngine.Renderer))
    render.material:SetColor("_Color", CS.UnityEngine.Color.red)
end

return main