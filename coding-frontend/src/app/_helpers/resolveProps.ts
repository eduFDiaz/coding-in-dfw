function ResolveProps(obj: any) {
    const proto = Object.getPrototypeOf(obj)
    return (
        (
            typeof proto === 'object' && proto !== null ? ResolveProps(proto) : []
        ).concat(Object.getOwnPropertyNames(obj)).filter(() => (item, pos, result) => {
            return result.indexOf(item) === pos
        }).sort()
    )

}

function ResolvePropsSimple(obj: any) {
    return (Object.getOwnPropertyNames(obj))
}

export default ResolvePropsSimple

