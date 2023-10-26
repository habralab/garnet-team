/* eslint-disable */
// @ts-nocheck
import { Context }   from '@ory/keto-namespace-types'
import { Namespace } from '@ory/keto-namespace-types'

class User implements Namespace {}

class Group implements Namespace {
  related: {
    members: User[]
  }
}

class Project implements Namespace {
  related: {
    viewers: Group[]
    owners: Group[]
  }

  permits = {
    view: (ctx: Context): boolean =>
      this.related.viewers.includes(ctx.subject) || this.related.owners.includes(ctx.subject),
    edit: (ctx: Context): boolean => this.related.owners.includes(ctx.subject),
  }
}

class Team implements Namespace {
  related: {
    viewers: Group[]
    owners: Group[]
  }

  permits = {
    view: (ctx: Context): boolean =>
      this.related.viewers.includes(ctx.subject) || this.related.owners.includes(ctx.subject),
    edit: (ctx: Context): boolean => this.related.owners.includes(ctx.subject),
  }
}
