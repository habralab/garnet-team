function(ctx) {
  identityId: ctx.identity.id,
  username:  ctx.identity.traits.name.first + " " + ctx.identity.traits.name.last
}